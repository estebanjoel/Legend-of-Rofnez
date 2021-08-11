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
    
    void Start()
    {
        SetTarget();
    }

    public void SetTarget()
    {
        character = FindObjectOfType<PlayerHealth>();
    }

    
    void Update()
    {
        if(character != null)
        {
            distance();
            if(distanceToPlayer< distanceToExlpote)
            {
                getPoisoned(character);
            }
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
    public override void getPoisoned(Health isPoison)
    {
        base.getPoisoned(isPoison);
        Destroy(gameObject);
    }
}
