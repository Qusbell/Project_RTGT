using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


/// <summary>
/// 각 Grid Cell에 대한 데이터 구조체
/// </summary>
public struct GridData
{
    /// <summary>
    /// 해당 Grid Cell을 점유 중인 개체 |
    /// Item(소지 가능한 물품 --> 내부에서 List 형태로 해서 EncounteredBy에서 호출하는 Pawn한테 다 넣어주면 되겠다),
    /// Pawn, Obstacle, Trigger 등
    /// </summary>
    public IGridEntity occupant;

    // <- 아직 다른 건 없지만, 나중을 위해서(확장성) GridData 구조체로 만들어두기
}


/// <summary>
/// Grid 관리 매니저
/// </summary>
[RequireComponent(typeof(Grid))]
public class GridLogicManager : MonoBehaviour
{
    /// <summary>
    /// 유니티 기본 제공 Grid
    /// </summary>
    private Grid m_gridSystem;
    private Grid GridSystem => m_gridSystem ??= GetComponent<Grid>();

    /// <summary>
    /// Grid Cell 별 데이터 |
    /// Vector3Int가 코드상으로는 편리하긴 해도, 잘못된 z값을 계속 경계해야 되는 걸 생각하면
    /// 그냥 Vector2Int 쓰고 무결성 유지하는 게 이득이라고 생각
    /// </summary>
    private Dictionary<Vector2Int, GridData> m_gridCells = new();


    /// <summary>
    /// transform을 Grid 위에 배치 & Init
    /// </summary>
    /// <param name="targetEntity">배치할 transform (this.transform)</param>
    /// <returns></returns>
    public bool InitOnGrid(IGridEntity targetEntity)
    {
        // ---- Grid 위로 배치 ---- //
        Vector3Int gridPos = GridSystem.WorldToCell(targetEntity.WorldPos);
        Vector3 worldPos = GridSystem.CellToWorld(gridPos);
        worldPos.z = worldPos.y; // 렌더링 순서
        targetEntity.WorldPos = worldPos;


        // ---- Cell 초기화 ---- //

        // 점유 대상 위치
        Vector2Int initPos = new Vector2Int(gridPos.x, gridPos.y);

        // 점유 시도 후 성공/실패 여부 반환
        return TryReserveOnGrid(initPos, targetEntity);
    }

    /// <summary>
    /// Grid 점유 시도
    /// </summary>
    /// <param name="targetGridPos">대상 Grid 위치</param>
    /// <param name="entity">점유할 Entity</param>
    /// <returns>true=점유 성공. false=점유 실패(이미 무언가 있음)</returns>
    public bool TryReserveOnGrid(Vector2Int targetGridPos, IGridEntity entity)
    {
        // 점유할 개체의 데이터 생성
        GridData ReserverData = new GridData();
        ReserverData.occupant = entity;
        // <-- 나중에 GridData가 더 많아지면, 다른 것들 초기화도 고려할 것

        // ---- 점유 시도 ---- //

        // 1. 이미 데이터가 존재하는 곳에 점유 시도
        if (m_gridCells.TryGetValue(targetGridPos, out GridData originData))
        {
            if (originData.occupant != null)
            {
                //Debug.Log($"점유 실패 | 이미 뭔가 존재함 | 자기자신 : {entity == originData.occupant}");
                return false;
            }
        }
        // 2. 아무 데이터도 없다면 = 점유
        else { m_gridCells[targetGridPos] = ReserverData; }

        // Entity의 Logical 좌표 이동
        // (Visual=World 좌표는 따로 계산)
        entity.GridPos = targetGridPos;
        //Debug.Log($"점유 성공 | 현재 좌표 : {entity.GridPos}");
        return true;
    }

    /// <summary>
    /// Grid Logic 상에서 제거
    /// </summary>
    /// <param name="clearTarget">대상 Target</param>
    public void ClearInGrid(IGridEntity clearTarget)
    {
        if (m_gridCells.TryGetValue(clearTarget.GridPos, out GridData data))
        {
            if (data.occupant == clearTarget)
            {
                data.occupant = null;
                m_gridCells[clearTarget.GridPos] = data;
            }
        }
    }

    /// <summary>
    /// Grid 좌표를 World 좌표로 반환
    /// </summary>
    /// <param name="targetGridPos">반환할 Grid 좌표</param>
    /// <returns></returns>
    public Vector3 GetGridToWorld(Vector2Int targetGridPos)
    {
        Vector3Int targetWorldPos = new Vector3Int(targetGridPos.x, targetGridPos.y, 0);
        return GridSystem.CellToWorld(targetWorldPos);
    }

    /// <summary>
    /// 대상 위치의 Entity 반환
    /// </summary>
    /// <param name="position">검사할 위치</param>
    /// <returns>대상 위치의 Entity</returns>
    public IGridEntity GetOccupantOnGrid(Vector2Int position)
    {
        if (m_gridCells.TryGetValue(position, out GridData data))
        { return data.occupant; }
        return null;
    }

    /// <summary>
    /// 대상 위치에 Encounter 시도
    /// </summary>
    /// <param name="pawn">Encounter를 실시하는 pawn</param>
    /// <param name="direction">pawn의 방향</param>
    /// <returns>이동 가능 여부 = isWalkable</returns>
    public bool TryEncounter(GridPawn pawn, Vector2Int direction)
    {
        // 방향이 없는 경우 : 조기 return
        if (direction == Vector2Int.zero) { return false; }


        // ---- 밑준비 ---- //

        // 대상 위치 계산
        Vector2Int targetPos = pawn.GridPos + direction;

        // 대상 위치로 이동 가능 여부 (기본 true)
        bool isWalkable = true;


        // ---- 실제 판정 ---- //

        // 대상 위치의 점유자(occupant) 호출
        IGridEntity targetOccupant = GetOccupantOnGrid(targetPos);

        // 점유자가 존재한다면 Encounter 발생
        if (targetOccupant != null)
        { isWalkable = targetOccupant.EncounteredBy(pawn); }

        return isWalkable;
    }

}