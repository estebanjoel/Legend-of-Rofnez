using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core
{
    public abstract class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;
        [SerializeField] float maxHealthPoints = 100f;
        [SerializeField] float currentTime;
        [SerializeField] float time;
        public AudioSource deadSound;
        public AudioClip deadClipSound;
        public AudioSource damageSound;
        public AudioClip damageClipSound;
        public AudioSource impactSound;
        public AudioClip impactClipSound;
        private bool isDead;
        public bool poisoned;
        public int tikDamage;
        
        private void Start()
        {
            healthPoints = maxHealthPoints;
        }


        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            ShowVisualChanges();
            damageSound.PlayOneShot(damageClipSound);
            impactSound.PlayOneShot(impactClipSound);
            if (healthPoints == 0)
            {
                Die();
            }

        }
        

        public abstract void ShowVisualChanges();
        public abstract void HealShader();

        public void Heal(float healPoints)
        {
            healthPoints = Mathf.Min(healthPoints + healPoints, maxHealthPoints);
            ShowVisualChanges();
            HealShader();
        }

        public float GetHP()
        {
            return healthPoints;
        }
        public float GetMaxHP()
        {
            return maxHealthPoints;
        }
        public void Die()
        {
            if (isDead) return;
            deadSound.PlayOneShot(deadClipSound);
            isDead = true;
            DeathBehaviour();
        }

        public abstract void DeathBehaviour();
    }
}