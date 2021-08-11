using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

public class Projectile : MonoBehaviour
{
    [SerializeField] Health target;
    [SerializeField] float speed;
    [SerializeField] private bool isHoming;
    float damage;
    [SerializeField] GameObject hitEffect = null;
    [SerializeField] float lifeSpan = 1f;
    float lifeTime = 0f;
    [Header("Audio Clips")]
    AudioManager audioManager;
    public AudioClip impactClip;
    public AudioClip launchClip;
    
    private void Start()
    {
        transform.LookAt(GetAimLocation());
        StartCoroutine(DestroyProjectileByLifeSpan());
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        
    }

    public void SetTarget(Health t, float d)
    {
        target = t;
        damage = d;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null) return;
        if(isHoming && !target.IsDead()) transform.LookAt(GetAimLocation());
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if(targetCapsule == null) return target.transform.position;
        return target.transform.position + Vector3.up * targetCapsule.height / 2;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool canBeDestroyed = false;
        if(other.gameObject == target.gameObject)
        {
            if(!target.IsDead() && !target.CheckInvencibility()) target.TakeDamage(damage);
            canBeDestroyed = ImpactEffect();
        }
        else if(other.gameObject.tag == "Prop" || other.gameObject.tag == "DestroyableObstacle")
        {
            canBeDestroyed = ImpactEffect();
        }
        if(canBeDestroyed)
        {
            DestroyProjectileByImpact();
        }
    }

    private bool ImpactEffect()
    {
        audioManager.TryToPlayClip(audioManager.obstacleSources, impactClip);
        if(hitEffect != null)
        {
            Instantiate(hitEffect, GetAimLocation(), transform.rotation);
        }
        return true;
    }  

    private IEnumerator DestroyProjectileByLifeSpan()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(gameObject);
    }

    private void DestroyProjectileByImpact()
    {
        speed = 0;
        StopCoroutine(DestroyProjectileByLifeSpan());
        Destroy(gameObject);
        
    }
}
