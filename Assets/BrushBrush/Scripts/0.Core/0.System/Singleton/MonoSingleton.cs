using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// MonoBehaviour 달려있는 싱글톤
/// </summary>
public abstract class MonoSingleton<T> : MonoBehaviour where T : Component
{
    public bool Initialized { get; private set; }

    protected static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<T>();
            }
            return instance;
        }
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //============================================================
    /// <summary>
    /// 자신을 생성한다. 
    /// </summary>
    public static void CreateSelf()
    {
        if (Instance != null)
        {
            return;
        }
        var go = new GameObject(typeof(T).Name);
        instance = go.AddComponent<T>();
    }


    /// <summary>
    /// 싱글톤 초기화
    /// </summary>
    public async UniTask InitAsync()
    {
        DontDestroyOnLoad(gameObject);
        await InitImpl();
        Initialized = true;
    }
    
    protected abstract UniTask InitImpl();



}