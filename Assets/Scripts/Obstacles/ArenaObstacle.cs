using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Obstacle
{
    public class ArenaObstacle : MonoBehaviour
    {
        [SerializeField] Transform door;
        [SerializeField] float newYRotation;
        [SerializeField] float rotationSpeed;
        [SerializeField] bool hasPlayerPickedUpTheKey = false;
        
        void Update()
        {
            Debug.Log(door.rotation.y);
            Debug.Log(newYRotation);
            if(hasPlayerPickedUpTheKey)
            {
                if(door.rotation.y <= newYRotation)
                {
                    Debug.Log("Rotating");
                    door.Rotate(new Vector3(0, -1 * rotationSpeed * Time.deltaTime, 0));
                }
            }
        }
        public void pickUpTheKey()
        {
            hasPlayerPickedUpTheKey = true;
        }
    }
}
