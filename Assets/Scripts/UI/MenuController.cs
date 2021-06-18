using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] GameObject pauseMenu;
        [SerializeField] GameObject fader;
        
        public GameObject GetPauseMenu()
        {
            return pauseMenu;
        }

        public GameObject GetFader()
        {
            return fader;
        }
        public void ShowUIObject(GameObject UIObject)
        {
            UIObject.SetActive(true);
        }

        public void HideUIObject(GameObject UIObject)
        {
            UIObject.SetActive(false);
        }
    }
}
