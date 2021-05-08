using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private ParticleSystem particle;
    private float currentTime;
    public float time = 2f;
    private float fireRank = 1;
    private void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        currentTime = time;
    }
    private void FixedUpdate()
    {
        currentTime -= Time.deltaTime;
        if(currentTime<=0)
        {
            particle.enableEmission = false;
            fireRank = 1;
            StartCoroutine(waitToReactivate());
           
        }
    }
    IEnumerator waitToReactivate()
    {
        yield return new WaitForSeconds(5);
        particle.enableEmission = true;
        fireRank += Time.deltaTime;
        Mathf.Clamp(fireRank, 0, 4);
        particle.startLifetime = fireRank;
        currentTime = time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("damage");
    }

   
}
