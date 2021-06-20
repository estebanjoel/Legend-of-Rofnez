using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    //Clase diseñada para el manejo de acciones de distintas clases en el juego
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction; //Acción actual activa
        
        //Si tengo una acción activa, la cancelo e inicio la nueva acción seleccionada
        public void StartAction(IAction action)
        {
            if(currentAction == action) return;
            if(currentAction != null) currentAction.Cancel();
            currentAction = action;
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }

        public IAction GetCurrentAction()
        {
            return currentAction;
        }
    }
    
}