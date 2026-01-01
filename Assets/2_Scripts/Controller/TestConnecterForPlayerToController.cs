using UnityEngine;

/// <summary>
/// Possess 테스트용 클래스
/// </summary>
public class TestConnecterForPlayerToController : MonoBehaviour
{
    void Start()
    {
        IAttemptable target = FindAnyObjectByType<GamePawn>();
        PlayerController controller = GetComponent<PlayerController>();

        if (target != null) { controller?.Possess(target); }
    }

}
