using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Deathcounter : MonoBehaviour
    {
        int counter = 0;

        public int AddToCounter()
        {
            counter++;
            return GetCounter();
        }

        public int GetCounter()
        {
            return counter;
        }

}
}
