using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MuverPlayer : MonoBehaviour
{
    
    [SerializeField] Transform target;
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoverAlCursor();
        }
        UpdateAnimator();
        if (Input.GetMouseButton(1))
        {
            Hitt();
        }
    }
    private void MoverAlCursor()//sitema de movimento
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
                if(hasHit == true)
                {GetComponent<NavMeshAgent>().destination = hit.point; }
        }
    private void UpdateAnimator()//el codigo de animacion de caminata
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 LocalVelocity = transform.InverseTransformDirection(velocity);
        float speed = LocalVelocity.z;
        GetComponent<Animator>().SetFloat("forwordSpeed",speed);
    }
    private void Hitt()
    {
        GetComponent<Animator>().SetTrigger("Attack");
    }
    void Hit()//Animator event para dano Proximamente
    {

    }
}
