using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class HealBar : MonoBehaviour
    {
        [SerializeField]Image healBar;
        public void ChangeBarFiller(float amount, float maxAmount)
        {
            healBar.fillAmount = amount / maxAmount;
        }

    }

}