using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogWarning($"Returning a null singleton instance of type {typeof(T)}, be wary of this race condition!");
            }
            return instance;
        }
    }

    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Debug.LogWarning("There's already an instance of this object in the scene... Destroying!");
            Destroy(this.gameObject);
        }
    }
}