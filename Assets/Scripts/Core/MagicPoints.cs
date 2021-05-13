using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

namespace RPG.Core
{
    public class MagicPoints : MonoBehaviour
    {
        [SerializeField] float magicPoints;
        [SerializeField] float maxMagicPoints;
        HealBar bar;
        // Start is called before the first frame update
        void Start()
        {
            magicPoints = maxMagicPoints;
            bar = GameObject.FindObjectsOfType<HealBar>()[0];
        }

        public float GetMagicPoints()
        {
            return magicPoints;
        }

        public void ConsumeMagicPoints(float mpToConsume)
        {
            magicPoints = Mathf.Max(magicPoints - mpToConsume, 0);
            bar.ChangeBarFiller(magicPoints, maxMagicPoints);
        }

        public void RestoreMagicPoints(float mptToRestore)
        {
            magicPoints = Mathf.Min(magicPoints + mptToRestore, maxMagicPoints);
            bar.ChangeBarFiller(magicPoints, maxMagicPoints);
        }
    }
}
