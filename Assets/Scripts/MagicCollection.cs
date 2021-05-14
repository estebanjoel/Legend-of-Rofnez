using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;

namespace RPG.Control
{
    public class MagicCollection : MonoBehaviour
    {
        [SerializeField] Magic[] collection = new Magic[5];

        public void AddMagic(Magic magic, int i)
        {
            collection[i] = magic;
        }

        public Magic GetMagicOnIndex(int i)
        {
            return collection[i];
        }
    }
}
