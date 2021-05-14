using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class EnemyHealth : Health
    {
        Renderer renderTexture;

        void Start()
        {
            renderTexture = transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>();
        }
        
        public override void ShowVisualChanges()
        {
            renderTexture.material.color = Color.red;
            StartCoroutine(BackToNormal());
        }

        public override void DeathBehaviour()
        {
            GameObject.FindObjectOfType<Deathcounter>().AddToCounter();
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public override void HealShader()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator BackToNormal()
        {
            yield return new WaitForSeconds(0.5f);
            renderTexture.material.color = Color.white;
        }
    }
}
