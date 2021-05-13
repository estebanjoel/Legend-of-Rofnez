using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using CombatEnums;

namespace RPG.Combat
{    

    [CreateAssetMenu(fileName = "Magic", menuName = "My Scriptable Objects/Make New Magic", order = 1)]
    public class Magic : ScriptableObject
    {
        [SerializeField] float magicDamage = 5;
        [SerializeField] float mpToConsume = 5;
        [SerializeField] float magicCooldown = 2f;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] MagicType magicType;

        public float GetMagicDamage()
        {
            return magicDamage;
        }
        
        public float GetMpToConsume()
        {
            return mpToConsume;
        }

        public float GetMagicCooldown()
        {
            return magicCooldown;
        }

        public GameObject GetEquippedPrefab()
        {
            return equippedPrefab;
        }

        public MagicType GetMagicType()
        {
            return magicType;
        }
    }
}
