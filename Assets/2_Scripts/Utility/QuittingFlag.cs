using UnityEngine;

/// <summary>
/// 비-제너릭 클래스. 애플리케이션 종료 플래그 관리
/// </summary>
public class QuittingFlag
{
    public static bool IsAppQuit { get; private set; } = false;

    public static void OnApplicationQuitting()
    { IsAppQuit = true; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    public static void InitializeOnLoad()
    {
        // Application.quitting 이벤트에 싱글톤의 종료 플래그 설정 메서드를 직접 연결합니다.
        Application.quitting -= QuittingFlag.OnApplicationQuitting; // 중복 방지
        Application.quitting += QuittingFlag.OnApplicationQuitting;
    }
}