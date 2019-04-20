using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAB{
    public class InputController : Singleton<InputController> {

        public bool isFire = false;

        public void StartFire() {
            if (isFire == false) isFire = true;
        }

        public void StopFire() {
            if (isFire == true) isFire = false;
        }

        public void Update()
        {

            if (Input.GetButton("Fire1"))
            {
                StartFire();
            }
            else if (isFire == true)
            {
                StopFire();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1)) {
                WeaponManger.Instance.ChangeWeapon(0);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                WeaponManger.Instance.ChangeWeapon(1);
            }
        }
    }
}
