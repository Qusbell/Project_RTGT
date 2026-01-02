using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 각 Grid Cell에 대한 데이터 구조체
/// </summary>
public struct GridData
{
    /// <summary>
    /// 해당 Grid Cell을 점유 중인 개체 |
    /// Item(소지 가능한 물품), Pawn, Obstacle, Trigger 등
    /// </summary>
    public IGridEntity occupant;

    // <- Drop Item 정보

}


/// <summary>
/// Grid 관리 매니저
/// </summary>
[RequireComponent(typeof(Grid))]
public class GridManager : MonoBehaviour
{
    /// <summary>
    /// 유니티 기본 제공 Grid
    /// </summary>
    private Grid m_gridSystem;
    public Grid GridSystem => m_gridSystem ??= GetComponent<Grid>();

    /// <summary>
    /// Grid Cell 별 데이터
    /// </summary>
    private Dictionary<Vector2Int, GridData> m_gridCells = new();



}
