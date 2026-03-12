using UnityEngine;

public abstract class BaseSingleton<T> : BaseMonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError($"{typeof(T)} Singleton not initialized!");
            return _instance;
        }
    }
    protected override void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
}
