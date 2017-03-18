using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GOStateMachineMonoSingle<T> : GameObjectStateMachineMMono where T: GOStateMachineMonoSingle<T>
{
    private static T instance = null;
    private static object lockObject = new object();

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                object obj = GOStateMachineMonoSingle<T>.lockObject;
                Monitor.Enter(obj);
                instance = GameObject.FindObjectOfType(typeof(T)) as T;
                if (instance == null)
                {
                    instance = new GameObject(typeof(T).Name, typeof(T)).GetComponent<T>();
                }
                Monitor.Exit(obj);
            }
            return instance;
        }
    }

    public virtual void SetDontDestroyOnLoad(Transform parent)
    {
        transform.parent = parent;
    }
}
