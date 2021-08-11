using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] GameObject pauseMenu;
        [SerializeField] GameObject fader;
        [SerializeField] GameObject weaponInventoryMenu;
        [SerializeField] GameObject currentWeaponActive;
        [SerializeField] GameObject ammoText;
        [SerializeField] GameObject godModeText;
        
        public GameObject GetPauseMenu()
        {
            return pauseMenu;
        }

        public GameObject GetFader()
        {
            return fader;
        }

        public GameObject GetWeaponInventoryMenu()
        {
            return weaponInventoryMenu;
        }

        public GameObject GetCurrentWeaponActive()
        {
            return currentWeaponActive;
        }

        public GameObject GetAmmoText()
        {
            return ammoText;
        }

        public GameObject GetGodModeText()
        {
            return godModeText;
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
