using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class Special : MonoBehaviour, IAction
    {
        [SerializeField] Transform rightHandTransform;
        [SerializeField] Transform leftHandTransform;
        Animator anim;
        MagicPoints magicPoints;
        [SerializeField] Magic defaultMagic = null;
        [SerializeField] Magic currentMagic = null;

        float timeToActivateMagic = Mathf.Infinity;

        void Start()
        {
            anim = GetComponent<Animator>();
            magicPoints = GetComponent<MagicPoints>();
            currentMagic = defaultMagic;
        }

        private void Update()
        {
            if(currentMagic == null) return;
            timeToActivateMagic += Time.deltaTime;    
        }

        public Magic getCurrentMagic()
        {
            return currentMagic;
        }

        public void SpecialAttack()
        {
            if(CheckIfCanUseMagic())
            {
                GetComponent<ActionScheduler>().StartAction(this);
                anim.SetTrigger("MagicAttack");
                magicPoints.ConsumeMagicPoints(currentMagic.GetMpToConsume());
                timeToActivateMagic = 0f;
            }
            
        }
        private bool CheckIfCanUseMagic()
        {
            bool firstCondition = currentMagic.GetMpToConsume() <= magicPoints.GetMagicPoints();
            bool secondCondition = timeToActivateMagic >= currentMagic.GetMagicCooldown();
            if(firstCondition && secondCondition) return true;
            else return false;
        }

        public void Cancel()
        {
            anim.ResetTrigger("MagicAttack");
            anim.SetTrigger("StopMagicAttack");
        }
    }
}
