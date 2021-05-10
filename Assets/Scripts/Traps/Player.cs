using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Damage
{
    public bool getTheKey = false;
    void Awake()
    {
        life = 2;
        maxLife = 4;
    }

    void dead()
    {
            Destroy(this.gameObject);   
    }
    void heal()
    {
        if(life < maxLife)
        {
            life++;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "KeyTag")
        {
            getTheKey = true;
        }
        else if (collision.collider.tag == "healObj")
        {
            heal();
            Destroy(collision.gameObject);
        }
            
        if (life <= 0)
        {
            dead();
        }


    }


}
