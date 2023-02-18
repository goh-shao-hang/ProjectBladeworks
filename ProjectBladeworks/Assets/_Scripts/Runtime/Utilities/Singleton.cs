using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Singleton<T> : MonoBehaviour where T: Singleton<T>
{
    private static T instance = null;
    private static T Instance => instance ??= FindObjectOfType<T>(); //new GameObject(typeof(T).ToString()).AddComponent<T>();

    private void OnApplicationQuit()
    {
        RemoveInstance();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private void RemoveInstance()
    {
        instance = null;
    }

    public static T GetInstance()
    {
        return Instance;
    }

    protected void SetDontDestroyOnLoad()
    {
        DontDestroyOnLoad(Instance.gameObject);
    }
}
