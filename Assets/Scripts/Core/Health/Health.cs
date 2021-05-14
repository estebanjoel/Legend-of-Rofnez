using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core
{
    public abstract class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;
        [SerializeField] float maxHealthPoints = 100f;
        private bool isDead;
        
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
            if (healthPoints == 0)
            {
                Die();
            }
        }

        public abstract void ShowVisualChanges();

        public void Heal(float healPoints)
        {
            healthPoints = Mathf.Min(healthPoints + healPoints, maxHealthPoints);
            ShowVisualChanges();
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

            isDead = true;
            DeathBehaviour();
        }

        public abstract void DeathBehaviour();
    }
}