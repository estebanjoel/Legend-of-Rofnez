using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

public class IronMeiden : MonoBehaviour
{
    [SerializeField] NavMeshAgent player3;
    [SerializeField] float goToPlayer;
    [SerializeField] float distanceToPlayer;
    [SerializeField] float speed;
    float closing;
    SphereCollider radius;
    float targetSpeed;
    Transform trapclosing;
    Transform closingPivot;
    [SerializeField] float speedClosing;


    void Start()
    {
        player3 = GameObject.FindWithTag("Player").GetComponent<NavMeshAgent>();
        radius = GetComponent<SphereCollider>();
        trapclosing = transform.GetChild(0);
        closingPivot = GameObject.FindWithTag("pivot").GetComponent<Transform>();
        radius.radius = goToPlayer;
        targetSpeed = player3.speed;
        closing = -3;
    }
    //cuando el personaje quede atrapado la velocidad la velocidad del player3 debe igualarse a 0 y luego volver a la normalidad con la variable targert
    private void Update()
    {
        distance();
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, goToPlayer);
        Gizmos.color = Color.green;
    }

    private void OnTriggerStay(Collider other)
    {
            
        
        if (distanceToPlayer < goToPlayer)
        {
            JumpToFace();
        }
    }

    void distance()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player3.transform.position);
    }

    void JumpToFace()
    {
        transform.LookAt(player3.transform);
        float velocity = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player3.transform.position,velocity);
        if (distanceToPlayer == 0)
        {
            glatony();
        }
    }

    void glatony()
    {
        if(closing <= trapclosing.rotation.y)
        {
            trapclosing.RotateAround(closingPivot.position, Vector3.up, speedClosing * Time.deltaTime);
        }
        
        player3.speed = 0;
    }
}
