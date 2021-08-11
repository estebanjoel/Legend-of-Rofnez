using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class WeaponInventorMenu : MonoBehaviour
    {
        MenuController menuController;
        [SerializeField] Sprite currentMeleeWeaponSprite;
        [SerializeField] Sprite currentRangedWeaponSprite;
        // Start is called before the first frame update
        void Start()
        {
            menuController = GetComponent<MenuController>();
        }

        public void SetCurrentWeaponActive(int currentWeapon)
        {
            Image activeWeaponImage = menuController.GetCurrentWeaponActive().GetComponent<Image>();
            switch(currentWeapon)
            {
                case 0:
                    activeWeaponImage.sprite = currentMeleeWeaponSprite;
                    menuController.HideUIObject(menuController.GetAmmoText());
                    break;
                case 1:
                    activeWeaponImage.sprite = currentRangedWeaponSprite;
                    menuController.ShowUIObject(menuController.GetAmmoText());
                    break;
                default:
                    activeWeaponImage.sprite = currentMeleeWeaponSprite;
                    menuController.HideUIObject(menuController.GetAmmoText());
                    break;
            }
        }

        public void SetMeleeWeaponSprite(Sprite sprite)
        {
            currentMeleeWeaponSprite = sprite;
        }

        public void SetRangedWeaponSprite(Sprite sprite)
        {
            currentRangedWeaponSprite = sprite;
        }

        public void SetAmmoText(string ammo)
        {
            menuController.GetAmmoText().GetComponent<Text>().text = "x"+ammo;
        }

    }

}