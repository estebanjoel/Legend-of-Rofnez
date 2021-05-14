using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class KillCounterText : MonoBehaviour
    {
        Text killCountText;

        void Start()
        {
            killCountText = GetComponent<Text>();
        }

        public void SetKillText(int counter)
        {
            killCountText.text = "Kills: " + counter.ToString();
        }
    }
}
