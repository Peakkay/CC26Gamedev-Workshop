using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    [Header("Singleton Settings")]
    [SerializeField] private bool dontDestroyOnLoad = true;

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;

            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Debug.LogWarning($"Duplicate singleton of type {typeof(T)} destroyed.");
            Destroy(gameObject);
        }
    }
}
