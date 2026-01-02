using System.Collections;
using UnityEngine;


public class GridPawn : GridEntity, IAttemptable
{
    // ==== Attempt Action ==== //

    /// <summary>
    /// 대상 방향으로 행동 시도
    /// (Move, Interact, Attack, Loot 등)
    /// </summary>
    /// <param name="direction">대상 방향</param>
    public void AttemptAction(Vector2Int direction)
    {
        // 조기 return
        if (isMoving) { return; }

        // direction 방향으로 EncounteredBy(this) 호출
        if (GridManager.TryEncounter(this, direction))
        {
            // 이동
            StartCoroutine(GridSmoothMovement(direction));
        }
    }

    // ==== Movement ==== //

    /// <summary>
    /// 이동 중인지 여부
    /// </summary>
    private bool isMoving = false;

    /// <summary>
    /// Grid 상에서 부드러운 이동
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private IEnumerator GridSmoothMovement(Vector2Int direction)
    {
        // 목적지 GridPos
        Vector2Int nowGridPos = GridPos;
        Vector2Int targetGrid = GridPos + direction;

        Vector3 startPos = transform.position;
        Vector3 endPos = GridManager.GetGridToWorld(targetGrid);
        endPos.z = endPos.y; // 렌더링 순서 : 화면 하단일수록 먼저 렌더링
        
        float current = 0f;
        float percent = 0f;

        float moveDuration = 0.2f;


        // 이동 시작 (출발)
        isMoving = true;

        // 현재 Grid 위치 점유 해제
        GridManager.ClearInGrid(this);
        // 도착 Grid 위치 점유 + GridPos 갱신
        GridManager.TryReserveOnGrid(targetGrid, this);

        while (percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / moveDuration;

            transform.position = Vector3.Lerp(startPos, endPos, percent);

            yield return null;
        }

        // 이동 종료 (도착)
        isMoving = false;

        // <- 도착 후, grid에 딱 맞게 위치 조정
    }


}
