using UnityEngine;
using UnityEngine.AI;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;
        [SerializeField] float maxHealthPoints = 100f;
        private bool isDead;
        public GameObject HealShader;

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
            if (healthPoints == 0)
            {
                Die();
            }
        }

        public void Heal(float healPoints)
        {
            healthPoints = Mathf.Min(healthPoints + healPoints, maxHealthPoints);
        }

        public float GetHP()
        {
            return healthPoints;
        }
        public void Die()
        {
            if (isDead) return;

            isDead = true;
            if (gameObject.tag == "Enemy" || gameObject.tag == "Player")
            {
                GetComponent<Animator>().SetTrigger("Die");
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }
            if (gameObject.tag == "DestroyableObstacle")
            {
                GetComponent<NavMeshObstacle>().enabled = false;
                GetComponent<MeshFilter>().mesh = null;
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        public void HealActiv()
        {
            HealShader.gameObject.SetActive(true);
        }
        public void HealDiActiv()
        {
            HealShader.gameObject.SetActive(false);
        }
    }
}