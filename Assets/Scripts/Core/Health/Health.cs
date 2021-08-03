using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;

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
        public ParticleSystem[] listFX;
        [SerializeField] bool activeFX; 
        private bool isDead;
        public bool poisoned;
        public int damagetik;
        public float poisonDamage;
        [SerializeField] Renderer poisonedColor;
        [SerializeField] Transform shaderSpawnpoint;
        bool isInvencible;
    

        public void ParentStartingSettings()
        {
            healthPoints = maxHealthPoints;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            //Antes de recibir daño, chequeo si puedo deflectar el daño en el caso de tener un escudo equipado
            if(GetComponent<Fighter>() == null)
            {
                DamageBehavoiur(damage);
            } 
            else
            {
                if(GetComponent<Fighter>().GetCurrentShield() != null)
                {
                    if (GetComponent<Fighter>().GetCurrentShield().DeflectDamage())
                    {
                        if(activeFX = false)
                        {
                            listFX[1].gameObject.SetActive(true);
                            listFX[2].gameObject.SetActive(true);
                            activeFX = true;
                        }
                        else
                        {
                            listFX[1].Play();
                            listFX[2].Play();
                        }
                        DamageBehavoiur(damage);
                    }
                }
                else
                {
                    DamageBehavoiur(damage);
                }
            }
        }

        private void DamageBehavoiur(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            ShowVisualChanges();
            impactSound.PlayOneShot(impactClipSound);
            damageSound.PlayOneShot(damageClipSound);
            if (healthPoints == 0)
            {
                Die();
            }
        }

        public void poisonDamages(int tik,float damage) // Función que setea el daño y el tik del veneno y ejecuta la corrutina tikDamage()
        {
            poisonDamage = damage;
            damagetik = tik;
            poisoned = true;
            poisonedColor.material.color = Color.green;
            StartCoroutine("tikDamage");
        }
        IEnumerator tikDamage() //Corrutina que restará daño al personaje por una cantidad igual al valor de 'damageTik'
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
            deadSound.PlayOneShot(deadClipSound);
            isDead = true;
            DeathBehaviour();
        }

        public abstract void DeathBehaviour();

        public void SpawnShader(GameObject shader) // Función para spawnear un efecto visual
        {
            GameObject newShader = GameObject.Instantiate(shader);
            newShader.transform.parent = shaderSpawnpoint;
            newShader.transform.position = shaderSpawnpoint.position;
            // newShader.transform.rotation = shaderSpawnpoint.rotation;
        }

        // Función para la invensibilidad

        public bool CheckInvencibility()
        {
            return isInvencible;
        }

        public void SetInvencibility(bool invencibility)
        {
            isInvencible = invencibility;
        }

        public IEnumerator DisableInvencibilityCo(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            SetInvencibility(false);
        }
    }
}