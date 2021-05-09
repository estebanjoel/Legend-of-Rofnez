using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damage : Lif
{
    public void TakeDamage(int damage)
    {
        life -= damage;
    }
}
