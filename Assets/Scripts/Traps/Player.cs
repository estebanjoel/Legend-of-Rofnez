using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Damage
{
    // Start is called before the first frame update
    void Start()
    {
        life = 2;
    }

    void dead()
    {
        if(life <= 0)
        Destroy(this.gameObject);
    }
}
