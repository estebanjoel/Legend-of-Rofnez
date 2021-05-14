using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using CombatEnums;

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
            setCurrentMagic(defaultMagic);
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

        public void setCurrentMagic(Magic magic)
        {
            currentMagic = magic;
            anim = currentMagic.SetAnimatorOverride(anim);
        }

        public void SpecialAttack()
        {
            if(CheckIfCanUseMagic())
            {
                GetComponent<ActionScheduler>().StartAction(this);
                anim.ResetTrigger("StopMagicAttack");
                anim.SetTrigger("MagicAttack");
                // InstantiateMagic();
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

        public void InstantiateMagic()
        {
            magicPoints.ConsumeMagicPoints(currentMagic.GetMpToConsume());
            if(currentMagic.GetMagicType() == MagicType.Expansive)
            {
                GameObject areaMagic = Instantiate(currentMagic.GetEquippedPrefab(), transform.position, transform.rotation);
                areaMagic.GetComponent<AreaMagic>().SetAreaDamage(currentMagic.GetMagicDamage());
            }
            if(currentMagic.GetMagicType() == MagicType.Projectile)
            {
                GameObject projectileMagic = Instantiate(currentMagic.GetEquippedPrefab(), leftHandTransform.position, leftHandTransform.rotation);
            }
        }

        public void Cancel()
        {
            anim.ResetTrigger("MagicAttack");
            anim.SetTrigger("StopMagicAttack");
        }
    }
}
