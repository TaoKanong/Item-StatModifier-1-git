using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    // Start is called before the first frame update
    public static T Instance;
    protected virtual void Awake()
    {
        Instance = this as T;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
