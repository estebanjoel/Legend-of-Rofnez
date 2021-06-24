using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogCoverable : MonoBehaviour
{
   [SerializeField] Renderer renderer;


    void Start()
    {
        //renderer = GetComponent<Renderer>();
        FieldOfView.OnTargetsVisibilityChange += FieldOfViewOnTargetsVisibilityChange;
    }

    void OnDestroy()
    {
        FieldOfView.OnTargetsVisibilityChange -= FieldOfViewOnTargetsVisibilityChange;
        Debug.Log("no render?");
    }

    void FieldOfViewOnTargetsVisibilityChange(List<Transform> newTargets)
    {
        renderer.enabled = newTargets.Contains(transform);
        Debug.Log("render?");
    }
}