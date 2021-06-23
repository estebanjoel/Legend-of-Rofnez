using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightVariation : MonoBehaviour
{
    [SerializeField] float flickerIntensity = 0.2f;
    [SerializeField] float flickersPerSecond = 3.0f;
    [SerializeField] float speedRandomness = 1.0f;

    [SerializeField] float time;
    [SerializeField] float startingIntensity;
    [SerializeField] Light light;

    void Start()
    {
        light = GetComponentInChildren<Light>();
        startingIntensity = light.intensity;
    }

    void Update()
    {
        time += Time.deltaTime * (1 - Random.Range(-speedRandomness, speedRandomness)) * Mathf.PI;
        light.intensity = startingIntensity + Mathf.Sin(time * flickersPerSecond) * flickerIntensity;

    }
}
