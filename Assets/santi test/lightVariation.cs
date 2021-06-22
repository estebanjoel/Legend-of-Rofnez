using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightVariation : MonoBehaviour
{
    [SerializeField] Light pointLight;
    [SerializeField] bool changeIntensity;
     float intensitySpeed;
    [SerializeField] float maxIntensity;
    [SerializeField] bool changeColor;
    float colorSpeed;
    [SerializeField] Color startColor;
    [SerializeField] Color endColor;
    void Start()
    {
        pointLight = GetComponentInChildren<Light>();
    }
    private void FixedUpdate()
    {
        
    }


    
}
