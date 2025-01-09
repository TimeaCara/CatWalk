using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : Component
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            if (TryGetComponent<T>(out T component))
            {
                Instance = component;
            }
            else
            {
                Debug.LogError("Singleton could not find designated MonoBehaviour on game-object!", this);
            }
        }
        else
        {
            Debug.LogError("Multiple instances of a Singleton detected within one scene. " +
                "All instances except the first one will be ignored.", this);
        }
    }
}
