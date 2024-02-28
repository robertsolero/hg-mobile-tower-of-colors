using UnityEngine;

public abstract class SingletonSpawnable<T> : MonoBehaviour where T : MonoBehaviour
{
    protected  abstract bool IsDontDestroyOnLoad { get; }
    
    private static T _instance;
    public static T Instance 
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                
                if (_instance != null)
                    return _instance;
                
                var go = new GameObject(typeof(T).Name);
                return _instance = go.AddComponent<T>();
            }

            return _instance;
        }
    }
    
    public virtual void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        if (IsDontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
    }
}