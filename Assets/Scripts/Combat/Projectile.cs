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
        bool canBeDestroyed = true;
        if(other.gameObject == target.gameObject)
        {
            if(!target.IsDead()) target.TakeDamage(damage);
            if(hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }
            else canBeDestroyed = false;
        }
        if(canBeDestroyed) Destroy(this.gameObject);
    }
}
