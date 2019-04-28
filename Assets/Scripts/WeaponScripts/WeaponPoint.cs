using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAB { 
    public class WeaponPoint : ItemBase {
        //Скрипт для лежащего на земле оружия

        public bool isPistol;

        protected override void Start()
        {
            base.Start();
            transform.position = transform.position + new Vector3(0, 0.27f, 0);
        }

        private void OnTriggerStay(Collider other)
        {
            if (Input.GetKeyDown(KeyCode.E)) { 
                WeaponManger manger = other.gameObject.GetComponent<WeaponManger>();
                if (manger != null)
                {
                    manger.GetUpWeapon(gameObject.name, isPistol);
                    Destroy(gameObject);
                }
            }
        }


    }
}
