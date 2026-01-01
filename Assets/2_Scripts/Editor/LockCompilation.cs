using UnityEditor;
using UnityEngine;

public class LockCompilation
{
    // 메뉴에 체크 표시를 하기 위한 변수
    private const string MenuPath = "Tools/Lock Compilation";

    [MenuItem(MenuPath)]
    public static void ToggleLock()
    {
        bool isLocked = EditorPrefs.GetBool("CompLock", false);

        if (!isLocked)
        {
            EditorApplication.LockReloadAssemblies(); // 리로드 봉쇄
            Debug.Log("❌ [컴파일 잠금] 이제 저장해도 리로드가 일어나지 않습니다.");
        }
        else
        {
            EditorApplication.UnlockReloadAssemblies(); // 리로드 해제
            Debug.Log("✅ [컴파일 해제] 이제 변경 사항이 반영됩니다.");
        }

        EditorPrefs.SetBool("CompLock", !isLocked);
    }
}
