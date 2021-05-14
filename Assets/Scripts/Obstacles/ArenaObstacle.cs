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
            if(hasPlayerPickedUpTheKey)
            {
                Quaternion worldRotation = transform.rotation * door.rotation;
                Debug.Log(worldRotation.y * newYRotation);
                if(worldRotation.y * newYRotation >= newYRotation/4)
                {
                    door.Rotate(new Vector3(0, -1 * rotationSpeed * Time.deltaTime, 0));
                    // Debug.Log(door.rotation.y * -newYRotation);
                }
            }
        }
        public void pickUpTheKey()
        {
            hasPlayerPickedUpTheKey = true;
        }
    }
}
