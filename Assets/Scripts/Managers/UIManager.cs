using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TAB { 

    public class UIManager : Singleton<UIManager>
    {
        public Image PrimariContainer, PimaryWeapon;
        public Image SecondaryContainer, SecondaryWeapon;
        public Text PrimaryAmmo;
        public Text SecondaryAmmo;

        public Image Hp;
        public Image Armour;
        public Text HpText;
        public Text ArmourText;

        public Text Score;

        WeaponManger wm;
        PlayerHealth player;

        private void Awake()
        {
            wm = WeaponManger.Instance;
            player = wm.gameObject.GetComponent<PlayerHealth>();
            PrimariContainer.gameObject.SetActive(false);
        }

        public void UpdateWeaponImage() {
            SecondaryWeapon.sprite = wm.invenntoryWeapon[0].GetComponent<Weapon>().UiImg;
            if (wm.invenntoryWeapon[1] == null)
            {
                PrimariContainer.gameObject.SetActive(false);
            }
            else {
                if(!PrimariContainer.gameObject.activeInHierarchy) PrimariContainer.gameObject.SetActive(true);
                PimaryWeapon.sprite = wm.invenntoryWeapon[1].GetComponent<Weapon>().UiImg;
            }
            UpdateAmmo();
        }

        public void UpdateAmmo() {
            SecondaryAmmo.text = string.Format("{0}/{1}", wm.invenntoryWeapon[0].GetComponent<Weapon>().Ammo, wm.invenntoryWeapon[0].GetComponent<Weapon>().AllAmmo);
            if (wm.invenntoryWeapon[1] != null) { 
            PrimaryAmmo.text = string.Format("{0}/{1}", wm.invenntoryWeapon[1].GetComponent<Weapon>().Ammo, wm.invenntoryWeapon[1].GetComponent<Weapon>().AllAmmo);
            }
        }

        public void UpdateScore() {
            Score.text = string.Format("Score: {0}", GameManager.Instance.score);
        }

        public void UpdateHp() {
 
            HpText.text = player.Hp.ToString();
            Hp.fillAmount = (float)player.Hp / (float)player.maxHp;
            ArmourText.text = player.Armour.ToString();
            Armour.fillAmount = (float)player.Armour / (float)player.maxArmour;
        }
    }
}
