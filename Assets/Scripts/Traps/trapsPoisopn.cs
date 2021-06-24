using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using RPG.Combat;

public class trapsPoisopn : poison
{
    [SerializeField] float distanceToExlpote;
    [SerializeField] float distanceToPlayer;
    [SerializeField] PlayerHealth character;
    [SerializeField] GameObject vFX;
    [SerializeField] float currentTime;
    [SerializeField] float time;
    
    void Start()
    {
        character = FindObjectOfType<PlayerHealth>();
        currentTime = time;
    }

    
    void Update()
    {
        distance();
        if(distanceToPlayer< distanceToExlpote)
        {
            getPoisoned();
        }
    }

    void distance()
    {
        distanceToPlayer = Vector3.Distance(transform.position, character.transform.position);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanceToExlpote);
    }
    public override void getPoisoned()
    {
        currentTime -= Time.deltaTime;
        if(currentTime <= 0)
        {
            vFX.SetActive(true);
            base.getPoisoned();
            Destroy(this.gameObject, 1.7f);
        }

        
    }
}
