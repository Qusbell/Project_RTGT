using UnityEngine;


public interface IAttemptable
{
    /// <summary>
    /// direction 방향으로 행동 시도를 구현
    /// </summary>
    /// <param name="direction">행동을 시도할 방향</param>
    public void Attempt(Vector2 direction);
}
