using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour, IPlayerControl
{
    // ==== Fields ==== //

    /// <summary>
    /// 대상이 될 Player |
    /// '행동 시도'가 가능한 개체
    /// </summary>
    private IAttemptable m_targetPlayer;

    /// <summary>
    /// 이전 벡터값
    /// </summary>
    private Vector2 prevVec = Vector2.zero;

    // ==== Methods ==== //

    /// <summary>
    /// Invoke Events 옵션을 통한 방향 입력 처리
    /// </summary>
    /// <param name="context">방향 정보</param>
    public void OnDirectionalAction(InputAction.CallbackContext context)
    {
        Vector2 inputVec = context.ReadValue<Vector2>();
        Debug.Log($"[PlayerController] : {context.ReadValue<Vector2>()}");

        // <- Player Turn
        // <- Charge Start

        // 모든 키를 뗐을 때
        if (inputVec == Vector2.zero)
        {
            // Int값으로 변환
            Vector3Int vector3Int = Vector3Int.zero;
            vector3Int.x = Mathf.RoundToInt(prevVec.x);
            vector3Int.y = Mathf.RoundToInt(prevVec.y);

            // 행동 시도
            // Release
            m_targetPlayer?.AttemptAction(vector3Int);
        }

        // 이전 벡터값 갱신
        prevVec = inputVec;
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
