using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.UI;

namespace RPG.Core
{
    public class PauseManager: MonoBehaviour
    {
        bool isPaused = false;

        public void SetPause(bool pauseState)
        {
            isPaused = pauseState;
            CheckPause();
        }

        public bool GetPauseState()
        {
            return isPaused;
        }

        public void CheckPause()
        {
            MenuController menuController = GameObject.FindObjectOfType<MenuController>();
            if(isPaused)
            {
                Time.timeScale = 0f;
                menuController.ShowUIObject(menuController.GetPauseMenu());
                menuController.HideUIObject(menuController.GetFader());
            }

            else
            {
                Time.timeScale = 1f;
                menuController.ShowUIObject(menuController.GetFader());
                menuController.HideUIObject(menuController.GetPauseMenu());
            } 
        }
    }
}
