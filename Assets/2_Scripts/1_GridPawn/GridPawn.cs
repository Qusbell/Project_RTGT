using UnityEngine;


public class GridPawn : MonoBehaviour, IAttemptable, IGridEntity
{
    // ==== Grid Entity ==== //
    private GridManager m_gridManager;
    public GridManager GridManager => m_gridManager ??= transform.root.GetComponent<GridManager>();

    public Vector2Int GridPos { get; set; }


    // ==== Attempt Action ==== //

    private Vector3Int targetGridPos;

    /// <summary>
    /// 대상 방향으로 행동 시도
    /// (Move, Interact, Attack, Loot 등)
    /// </summary>
    /// <param name="direction">대상 방향</param>
    public void AttemptAction(Vector3Int direction)
    {

    }

    private void Update()
    {

    }

    // ==== Encounterable ==== //

    public bool EncounteredBy(GridPawn actor)
    {
        return false;
    }

}
