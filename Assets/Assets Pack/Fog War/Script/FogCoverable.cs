using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogCoverable : MonoBehaviour
{
    [SerializeField] Renderer[] renderers;


    void Start()
    {
        //renderer = GetComponent<Renderer>();
        FieldOfView.OnTargetsVisibilityChange += FieldOfViewOnTargetsVisibilityChange;
    }

    void OnDestroy()
    {
        FieldOfView.OnTargetsVisibilityChange -= FieldOfViewOnTargetsVisibilityChange;
    }

    void FieldOfViewOnTargetsVisibilityChange(List<Transform> newTargets)
    {
        for (int i=0; i<renderers.Length;i++ )
        {
            renderers[i].enabled = newTargets.Contains(transform);
        }
    }
}