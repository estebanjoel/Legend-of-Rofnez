using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Shield", menuName = "My Scriptable Objects/Make New Shield", order = 2)]
    public class Shield : ScriptableObject
    {
        [SerializeField] GameObject equippedPrefab;
        [SerializeField] int shieldDeflectionPercentage = 5;

        const string shieldName = "Shield";

        public void SpawnShield(Transform leftHand)
        {
            GameObject newWeapon = Instantiate(equippedPrefab, leftHand);
            newWeapon.name = shieldName;
        }

        public bool DeflectDamage()
        {
            int deflectionPercentage = Random.Range(0, 101);
            if (deflectionPercentage <= shieldDeflectionPercentage) return true;
            else return false;
        }
    }
}
