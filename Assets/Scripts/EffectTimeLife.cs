using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTimeLife : MonoBehaviour
{
    float lifeTime;
    ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        lifeTime = ps.duration;
        StartCoroutine(DestroyEffect());
    }

    private IEnumerator DestroyEffect()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
