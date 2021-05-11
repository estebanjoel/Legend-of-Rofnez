using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    [RequireComponent(typeof(Health))] //Cada vez que agregues el componente CombatTarget, Health se agregará automáticamente. Y si intentas borrar Health de un componente con CombatTarget, Unity no te va a dejar
    public class CombatTarget : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
