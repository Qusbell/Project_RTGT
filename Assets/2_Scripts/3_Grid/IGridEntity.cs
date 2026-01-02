using UnityEngine;

/// <summary>
/// Grid 시스템에서, 그리드 위에 존재하는 개체
/// </summary>
public interface IGridEntity
{
    /// <summary>
    /// Grid 초기화 --> 의존성 주입
    /// </summary>
    /// <param name="grid">부모 Grid</param>
    public bool InitGridSystem(Grid grid);
}
