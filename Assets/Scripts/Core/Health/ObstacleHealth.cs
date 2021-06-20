using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core
{
    public class ObstacleHealth : Health
    {
        public override void ShowVisualChanges()
        {
            Debug.Log(GetHP());
        }
        
        public override void DeathBehaviour()
        {
            GetComponent<NavMeshObstacle>().enabled = false;
            GetComponent<MeshFilter>().mesh = null;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
