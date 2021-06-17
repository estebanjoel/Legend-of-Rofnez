using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class CamaraFollower : MonoBehaviour
    {
        [SerializeField] float camSpeed;
        [SerializeField] float mauseBorderMuv;
        [SerializeField] private Transform target;
        private void Start()
        {
            transform.position = target.position;
        }
        void Update()
        {
            if (Input.GetKey("space"))
            {
                transform.position = target.position;
            }else
            {
                Vector3 pos = transform.position;
                if (Input.mousePosition.y >= Screen.height - mauseBorderMuv)//w
                {
                    pos.z += camSpeed * Time.deltaTime;
                }
                if (Input.mousePosition.y <= mauseBorderMuv)//s
                {
                    pos.z -= camSpeed * Time.deltaTime;
                }
                if (Input.mousePosition.x >= Screen.width - mauseBorderMuv)//d
                {
                    pos.x += camSpeed * Time.deltaTime;
                }
                if (Input.mousePosition.x <= mauseBorderMuv)//a
                {
                    pos.x -= camSpeed * Time.deltaTime;
                }
                transform.position = pos;
            }
        }
    }

}
