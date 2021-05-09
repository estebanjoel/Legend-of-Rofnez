using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private ParticleSystem particle;
    private float currentTime;
    public float time = 2f;
    private float fireRank = 1;
    BoxCollider objCollider;
    private void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        objCollider = GetComponent<BoxCollider>();
        currentTime = time;
    }
    private void FixedUpdate()
    {
        currentTime -= Time.deltaTime;
        if(currentTime<=0)
        {
            particle.enableEmission = false;
            fireRank = 1;
            objCollider.enabled = false;
            StartCoroutine(waitToReactivate());
           
        }
    }
    IEnumerator waitToReactivate()
    {
        
        yield return new WaitForSeconds(5);
        objCollider.enabled = true;
        particle.enableEmission = true;
        fireRank += Time.deltaTime;
        Mathf.Clamp(fireRank, 0, 4);
        particle.startLifetime = fireRank;
        currentTime = time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Player>().TakeDamage(1);
    }

}
