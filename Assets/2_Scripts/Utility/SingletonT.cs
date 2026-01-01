using UnityEngine;

/// <summary>
/// 제너릭 싱글톤 베이스 클래스
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonT<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    /// <summary>
    /// Instance 접근자.
    /// I로 축약해서 사용
    /// </summary>
    public static T Instance
    {
        get
        {
            // 종료 중 검토
            if (QuittingFlag.IsAppQuit)
            {
#if UNITY_EDITOR
                Debug.Log($"{typeof(T)} : 인스턴스가 파괴되었거나 앱 종료 중이므로 생성하지 않습니다.");
#endif
                return null;
            }

            // 인스턴스가 없으면 씬에서 찾아보거나 새로 생성
            if (instance == null || instance.Equals(null))
            {
                instance = Object.FindFirstObjectByType<T>();
                if (instance == null)
                {
                    GameObject singletonObj = new GameObject();
                    instance = singletonObj.AddComponent<T>();
                    singletonObj.name = typeof(T).ToString() + " (Singleton)";
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(instance.gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            DontDestroyOnLoad(instance.gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        { instance = null; }
    }

}