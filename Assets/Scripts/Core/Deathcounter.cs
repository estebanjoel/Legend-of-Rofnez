using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

namespace RPG.Core
{
    public class Deathcounter : MonoBehaviour
    {
        int counter = 0;

        public int AddToCounter()
        {
            counter++;
            GameObject.FindObjectOfType<KillCounterText>().SetKillText(counter);
            return GetCounter();
        }

        public int GetCounter()
        {
            return counter;
        }

}
}
