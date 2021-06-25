using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

namespace RPG.Core
{
    public class WizardBossHealth : Health
    {
        [Header("Tornado Variables")]
        float currentHP; //HP Actual del Boss
        float currentAmount = 0; //Cantidad actual para la acción del tornado
        [SerializeField] float hpAmountToDoATornado = 50f; // Cantidad necesaria para ejecutar el tornado
        float timesTornadoWasMade = 0; // Veces que el tornado se haya ejecutado
        [Header("Health Bar")]
        [SerializeField] HealBar bar;
        void Start()
        {
            ParentStartingSettings();
            currentHP = GetHP();
            SetHealthBar();
        }

        public void SetHealthBar()
        {
            HealBar[] healbars = GameObject.FindObjectsOfType<HealBar>();
            for (int i = 0; i < healbars.Length; i++)
            {
                if (healbars[i].gameObject.name == "BossHealthBar")
                {
                    bar = healbars[i];
                    break;
                }
            }
        }

        public bool CheckIfICanDoATornadoMovement()
        {
            if(IsDead()) return false;
            if(currentHP == GetHP()) return false;
            else
            {
                ChangeHPAmountToDoSomeMechanic();
                if(currentAmount >= hpAmountToDoATornado)
                {
                    currentAmount = 0;
                    timesTornadoWasMade++;
                    return true;
                } 
                else return false;
            }
        }

        private void ChangeHPAmountToDoSomeMechanic()
        {
            currentAmount += GetMaxHP() - GetHP() - currentAmount - hpAmountToDoATornado * timesTornadoWasMade;
            currentHP = GetHP();
            Debug.Log(currentAmount);
            
        }

        public override void DeathBehaviour()
        {
            GetComponent<Animator>().SetTrigger("Die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public override void ShowVisualChanges()
        {
            bar.ChangeBarFiller(GetHP(), GetMaxHP());
        }

    }
}
