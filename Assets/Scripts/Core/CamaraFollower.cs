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
        float[] levelBounds;
        private void Start()
        {
            transform.position = target.position;
            SetCurrentLevelBounds();
        }

        // Asigna los bounds del nivel actual
        private void SetCurrentLevelBounds()
        {
            levelBounds = GameObject.FindObjectOfType<LevelBounds>().GetBounds();
        }

        void Update()
        {
            if (Input.GetKey("space"))
            {
                transform.position = target.position;
            }
            else
            {
                Vector3 pos = transform.position;
                float axisMod;
                //Chequeamos si la posicion del mouse supera el ancho o el largo de la pantala y si tenemos algun axis activo (Horizontal y Vertical)
                if (Input.mousePosition.x <= mauseBorderMuv || Input.GetAxis("Horizontal") <= -0.5f)//izquierda
                {
                    if(Input.GetAxis("Horizontal")!=0) axisMod = Input.GetAxis("Horizontal");
                    else axisMod = -1;
                    pos.x += camSpeed * Time.deltaTime * axisMod;
                    if(pos.x < levelBounds[0]) pos.x = levelBounds[0];
                }

                if (Input.mousePosition.x >= Screen.width - mauseBorderMuv || Input.GetAxis("Horizontal") >= 0.5f)//derecha
                {
                    if(Input.GetAxis("Horizontal")!=0) axisMod = Input.GetAxis("Horizontal");
                    else axisMod = 1;
                    pos.x += camSpeed * Time.deltaTime * axisMod;
                    if(pos.x > levelBounds[1]) pos.x = levelBounds[1];
                }

                if (Input.mousePosition.y >= Screen.height - mauseBorderMuv || Input.GetAxis("Vertical") >= 0.5f)//arriba
                {
                    if(Input.GetAxis("Vertical")!=0) axisMod = Input.GetAxis("Vertical");
                    else axisMod = 1;
                    pos.z += camSpeed * Time.deltaTime * axisMod;
                    if(pos.z > levelBounds[2]) pos.z = levelBounds[2];
                }

                if (Input.mousePosition.y <= mauseBorderMuv || Input.GetAxis("Vertical") <= -0.5f)//abajo
                {
                    if(Input.GetAxis("Vertical")!=0) axisMod = Input.GetAxis("Vertical");
                    else axisMod = -1;
                    pos.z += camSpeed * Time.deltaTime * axisMod;
                    if(pos.z < levelBounds[3]) pos.z = levelBounds[3];
                }

                transform.position = pos;
            }
        }
    }
}
