using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

public class IronMeiden : MonoBehaviour
{
    [SerializeField] PlayerHealth player3;
    [SerializeField] float goToPlayer;
    [SerializeField] float distanceToPlayer;

    void Start()
    {
        player3 = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player3)
        {
            distance();
        }

    }
     
    void distance()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player3.transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, goToPlayer);
        Gizmos.color = Color.green;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(distanceToPlayer < goToPlayer)
        {
          
        }
    }
}
