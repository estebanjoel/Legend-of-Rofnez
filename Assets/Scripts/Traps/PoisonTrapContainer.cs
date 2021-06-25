using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTrapContainer : MonoBehaviour
{
    [SerializeField] trapsPoisopn[] trapsPoisopns;
    
    public trapsPoisopn[] GetTrapsPoisopns()
    {
        return trapsPoisopns;
    }
}
