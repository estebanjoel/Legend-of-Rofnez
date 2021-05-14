using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Deathcounter : MonoBehaviour
    {
        int counter = 0;

        public float AddToCounter()
        {
            counter++;
            return GetCounter();
        }

        public float GetCounter()
        {
            return counter;
        }

}
}
