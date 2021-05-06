using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MuverPlayer : MonoBehaviour
{
    
    [SerializeField] Transform target;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoverAlCursor();
            //LastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
    private void MoverAlCursor()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
                if(hasHit == true)
                {GetComponent<NavMeshAgent>().destination = hit.point; }
        }
}
