using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAB { 
    public class WeaponManger : Singleton<WeaponManger> {
        //Скрипт смены оружия 

        public GameObject[] WeaponsList; // Список всего возможного оружия
        private Dictionary<string, GameObject> Weapons = new Dictionary<string, GameObject>(); // переводим в словарь для более удобной работы
        public GameObject CurrentWeapon;//Ссылка на текущий объект оружия
        public GameObject[] invenntoryWeapon = new GameObject[2]; //оружие в слотах

        Animator anim; //Аниматор персонажа
        public enum WeaponAnimation // enum для определения анимации подходящей текущеему оружию
        {
            isPistol,
            isRifle
        }

	    // Use this for initialization
	    void Start () {
            anim = GetComponent<Animator>(); // получаем ссылку на аниматор
            foreach (var weapon in WeaponsList) // добавляем все оружие в словарь
            {
                weapon.SetActive(false);
                Weapons.Add(weapon.name, weapon);
            }
            GetUpWeapon(WeaponsList[0].name, true);
	    }

        //Функция замены оружия
        public void ChangeWeapon(int slot) {

            if (invenntoryWeapon[slot] == null)  return; 

            if(CurrentWeapon != null) { CurrentWeapon.SetActive(false); }
            CurrentWeapon = invenntoryWeapon[slot];
            CurrentWeapon.SetActive(true);
            ChangePose(CurrentWeapon.GetComponent<Weapon>().WeaponAnimation);
        }

        //Меняем анимацию в соответствии нового оружия
        private void ChangePose(WeaponAnimation animation) {
            anim.SetBool("isRifle",false);
            anim.SetBool("isPistol", false);
            switch (animation)
            {
                case WeaponAnimation.isPistol:
                    anim.SetBool("isPistol", true);
                    break;
                case WeaponAnimation.isRifle:
                    anim.SetBool("isRifle", true);
                    break;
                default:
                    throw new System.Exception("Не веро задана анимация");

            }
        }

        public void GetUpWeapon(string weaponName, bool isPistol) {
            Debug.Log(Weapons[weaponName].name);
            if (isPistol)
            {
                invenntoryWeapon[0] = Weapons[weaponName];
                ChangeWeapon(0);
            }
            else
            {
                invenntoryWeapon[1] = Weapons[weaponName];
                ChangeWeapon(1);
            }

        }
    }
}
