using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Item
{
    public abstract class ItemPickup : MonoBehaviour
    {
        AudioSource audioSource;
        [SerializeField] AudioClip itemClip;

        void Start()
        {
            audioSource = GameObject.Find("PickupSource").GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                audioSource.clip = itemClip;
                audioSource.Play();
                UseItem(other.gameObject);
                Destroy(this.gameObject);
            }    
        }

        public abstract void UseItem(GameObject player);
    }
}
