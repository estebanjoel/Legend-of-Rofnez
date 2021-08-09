using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField] float leftAttackLimit;
    [SerializeField] float rightAttackLimit;
    [SerializeField] float frontAttackLimit;
    [SerializeField] float backAttackLimit;

    public float GetLeftAttackLimit()
    {
        return leftAttackLimit;
    }

    public float GetRightAttackLimit()
    {
        return rightAttackLimit;
    }

    public float GetFrontAttackLimit()
    {
        return frontAttackLimit;
    }

    public float GetBackAttackLimit()
    {
        return backAttackLimit;
    }
}
