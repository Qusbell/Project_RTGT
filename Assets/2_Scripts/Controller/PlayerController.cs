using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour, IPlayerControl
{
    // ==== Fields ==== //

    /// <summary>
    /// 대상이 될 Player
    /// </summary>
    private IAttemptable m_targetPlayer;
     
    // ==== Methods ==== //

    /// <summary>
    /// Invoke Events 옵션을 통한 방향 입력 처리
    /// </summary>
    /// <param name="context">방향 정보</param>
    public void OnDirectionalAction(InputAction.CallbackContext context)
    {
        // 입력 벡터 값 읽기
        Vector2 inputVec = context.ReadValue<Vector2>();

        // 임시 : 입력 started 시에만 시도
        // ToDo : 특정 방향 입력 도중, 다른 방향의 입력이 들어올 경우 -> Last Input Priority 처리
        if (context.started) { m_targetPlayer?.Attempt(inputVec); }
    }


    /// <summary>
    /// Controller를 통한 target에 대한 빙의
    /// </summary>
    /// <param name="target">빙의 대상</param>
    public void Possess(IAttemptable target)
    {
        // 기존 대상이 있을 경우 빙의 해제
        if (m_targetPlayer != null) { Unpossess(); }

        // 대상 설정
        m_targetPlayer = target;
    }

    /// <summary>
    /// Controller의 빙의 해제
    /// </summary>
    public void Unpossess()
    {
        // 대상 초기화
        m_targetPlayer = null;
    }

}
