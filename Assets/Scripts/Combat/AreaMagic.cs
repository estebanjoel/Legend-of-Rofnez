using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    public class AreaMagic : MonoBehaviour
    {
        private float areaDamage;
        private float lifeSpan;
        private float lifeTime = 0f;
        private ParticleSystem particle;
        void Start()
        {
            particle = transform.GetComponentInChildren<ParticleSystem>();
            lifeSpan = particle.duration;
        }

        // Update is called once per frame
        void Update()
        {
            lifeTime += Time.deltaTime;
            if(lifeTime >= lifeSpan) Destroy(this.gameObject);
        }

        public void SetAreaDamage(float damage)
        {
            areaDamage = damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "DestroyableObstacle")
            {
                other.GetComponent<Health>().TakeDamage(areaDamage);
            }
        }
    }
}
