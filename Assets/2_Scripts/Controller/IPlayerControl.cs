using UnityEngine.InputSystem;


/// <summary>
/// [조건] Player Input 컴포넌트의 Invoke Events 옵션 사용 시
/// [역할] Player에 대한 조작 input을 처리
/// </summary>
public interface IPlayerControl
{
    public void OnDirectionalAction(InputAction.CallbackContext context);
}
