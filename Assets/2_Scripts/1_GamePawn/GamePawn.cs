using UnityEngine;


public class GamePawn : MonoBehaviour, IAttemptable, IGridEntity
{
    // ==== Grid System ==== //

    /// <summary>
    /// 유니티 기본 제공 Grid
    /// </summary>
    private Grid m_gridSystem;

    /// <summary>
    /// Grid 시스템 초기화
    /// </summary>
    /// <param name="grid">대상 Grid</param>
    /// <returns></returns>
    public bool InitGridSystem(Grid grid)
    {
        m_gridSystem = grid;
        return grid != null;
    }

    private void OnEnable()
    {
        Grid grid = transform.root.GetComponent<Grid>();
        if (!InitGridSystem(grid))
        {
#if UNITY_EDITOR
            Debug.LogError($"[{name}] Grid 시스템 초기화 실패 : GamePawn 파괴");
#endif
            Destroy(this);
        }
    }


    // ==== Attempt Action ==== //

    private Vector3Int CurrentGridPos => m_gridSystem.WorldToCell(transform.position);
    private Vector3Int targetGridPos;

    /// <summary>
    /// 대상 방향으로 행동 시도
    /// (Move, Interact, Attack, Loot 등)
    /// </summary>
    /// <param name="direction">대상 방향</param>
    public void AttemptAction(Vector3Int direction)
    {
        targetGridPos = CurrentGridPos + direction;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetGridPos, 10f * Time.deltaTime);
    }

}
