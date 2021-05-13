using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

namespace RPG.Core
{
    public class PlayerHealth : Health
    {
        HealBar bar;
        [SerializeField] GameObject HealShader;

        private void Start()
        {
            bar = GameObject.FindObjectsOfType<HealBar>()[1];    
        }

        public override void ShowVisualChanges()
        {
            bar.ChangeBarFiller(GetHP(), GetMaxHP());
        }

        public override void DeathBehaviour()
        {
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public void HealActiv()
        {
            HealShader.gameObject.SetActive(true);
        }
        public void HealDiActiv()
        {
            HealShader.gameObject.SetActive(false);
        }
    }
}
