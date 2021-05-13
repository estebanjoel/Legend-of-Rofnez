using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{    
    enum MagicType
    {
        Projectile,
        Expansive,
        Melee,
        Defensive,
        Heal
    }

    [CreateAssetMenu(fileName = "Magic", menuName = "My Scriptable Objects/Make New Magic", order = 1)]
    public class Magic : ScriptableObject
    {
        [SerializeField] float magicDamage = 5;
        [SerializeField] float mpToConsume = 5;
        [SerializeField] float magicCooldown = 2f;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] MagicType magicType;

        public float GetMpToConsume()
        {
            return mpToConsume;
        }

        public float GetMagicCooldown()
        {
            return magicCooldown;
        }
    }
}
