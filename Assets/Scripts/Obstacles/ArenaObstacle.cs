using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

namespace RPG.Obstacle
{
    public class ArenaObstacle : MonoBehaviour
    {
        [SerializeField] Transform door;
        [SerializeField] float newYRotation;
        [SerializeField] float rotationSpeed;
        [SerializeField] bool hasPlayerPickedUpTheKey = false;
        EventText eventText;

        private void Start()
        {
            eventText = GameObject.FindObjectOfType<EventText>();
        }
        
        void Update()
        {
            if(hasPlayerPickedUpTheKey)
            {
                Quaternion worldRotation = transform.rotation * door.rotation;
                Debug.Log(worldRotation.y * newYRotation);
                Debug.Log(newYRotation/4);
                if(worldRotation.y * newYRotation >= newYRotation/4)
                {
                    Debug.Log("rotating");
                    door.Rotate(new Vector3(0, -1 * rotationSpeed * Time.deltaTime, 0));
                }
            }
        }
        public void pickUpTheKey()
        {
            hasPlayerPickedUpTheKey = true;
            eventText.SetEventText("You can enter the Arena and fight the boss!");
        }
    }
}
