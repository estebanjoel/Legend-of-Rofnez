using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton instance;
    void Start()
    {
        if(instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(instance);
    }
}
