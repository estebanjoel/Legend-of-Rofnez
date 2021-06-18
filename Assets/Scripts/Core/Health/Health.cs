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
        public AudioSource deadSound;
        public AudioClip deadClipSound;
        public AudioSource damageSound;
        public AudioClip damageClipSound;
        public AudioSource impactSound;
        public AudioClip impactClipSound;
        private bool isDead;
        public bool poisoned;
        public int damagetik;
        public float poisonDamage;
        public Renderer poisonedColor;
        
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
        public void poisonDamages(int tik,float damage)
        {
            poisonDamage = damage;
            damagetik = tik;
            poisoned = true;
            poisonedColor.material.color = Color.green;
            StartCoroutine("tikDamage");
        }
        IEnumerator tikDamage()
        {
            for(int i = 0; i < damagetik; i++)
            {
                yield return new WaitForSeconds(1f);
                TakeDamage(poisonDamage);

            }
            poisoned = false;
            poisonedColor.material.color = Color.white;
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