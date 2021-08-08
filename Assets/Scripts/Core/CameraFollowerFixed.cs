using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
        public class CameraFollowerFixed : MonoBehaviour
    {
        [SerializeField] float camSpeed;
        [SerializeField] private Transform target;
        
        private void Start()
        {
            SetCameraStartingSettings();
        }

        public void SetCameraStartingSettings()
        {
            transform.position = target.position;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = target.position;
        }
    }

}