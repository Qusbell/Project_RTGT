using UnityEngine;

public class GridEntity : MonoBehaviour, IGridEntity
{
    private GridLogicManager m_gridManager;
    public GridLogicManager GridManager => m_gridManager ??= transform.root.GetComponent<GridLogicManager>();

    public Vector2Int GridPos { get; set; }

    public Vector3 WorldPos
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    public bool EncounteredBy(GridPawn actor)
    {
        return false;
    }


    private void OnEnable()
    {
        GridManager.InitOnGrid(this);
    }

    private void OnDisable()
    {
        GridManager.ClearInGrid(this);
    }

}
