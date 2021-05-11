using UnityEngine;
using UnityEngine.AI;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;
        private bool isDead;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if(healthPoints == 0)
            {
                Die();
            }
        }

        public float GetHP()
        {
            return healthPoints;
        }
        public void Die()
        {
            if(isDead) return;
            
            isDead = true;
            GetComponent<Animator>().SetTrigger("Die");
        }
    }
}