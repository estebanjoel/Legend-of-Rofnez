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
    
    private void Start()
    {
        transform.LookAt(GetAimLocation());
    }

    public void SetTarget(Health t, float d)
    {
        target = t;
        damage = d;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        if(lifeTime < lifeSpan)
        {
            if(target == null) return;
            if(isHoming && !target.IsDead()) transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
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
            if(!target.IsDead()) target.TakeDamage(damage);
            if(hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }
            canBeDestroyed = true;
        }
        else if(other.gameObject.tag == "Prop" || other.gameObject.tag == "Obstacle")
        {
            canBeDestroyed = true;
            if(hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, transform.rotation);
            }
        }
        if(canBeDestroyed)
        {
            Destroy(this.gameObject);
        }
    }
}
