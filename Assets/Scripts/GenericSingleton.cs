using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSingleton <T>: MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance 
    {
        get 
        {
            if (_instance == null) 
            {
                _instance = FindObjectOfType<T> ();
                if (_instance == null) 
                {
                    GameObject obj = new GameObject {name = typeof(T).Name};
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
 
    public virtual void Awake ()
    {
        if (_instance == null) 
        {
            _instance = this as T;
            DontDestroyOnLoad (this.gameObject);
        } else 
        {
            Destroy (gameObject);
        }
    }
}
