using UnityEngine;


public class GamePawn : MonoBehaviour, IAttemptable
{
    /// <summary>
    /// 대상 방향으로 행동 시도
    /// (Move, Interact, Attack, Loot 등)
    /// </summary>
    /// <param name="direction">대상 방향</param>
    public void Attempt(Vector2 direction)
    {
        // Test : 대상 방향으로 이동
        transform.Translate(direction);
    }



}
