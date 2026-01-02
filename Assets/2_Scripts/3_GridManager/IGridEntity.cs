using UnityEngine;

/// <summary>
/// Grid 시스템에서, 그리드 위에 존재하는 개체 |
/// 모든 GridEntity가 보유해야 하는 인터페이스
/// </summary>
public interface IGridEntity : IEncounterable
{
    /// <summary>
    /// transform.root에 존재하는 Grid 관리자
    /// </summary>
    public GridManager GridManager { get; }

    /// <summary>
    /// 해당 개체가 위치한 Grid 상에서의 Logical 좌표
    /// </summary>
    public Vector2Int GridPos { get; set; }
}
