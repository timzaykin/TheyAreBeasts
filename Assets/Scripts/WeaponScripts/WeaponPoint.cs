using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAB { 
    public class WeaponPoint : MonoBehaviour {
        //Скрипт для лежащего на земле оружия

        public bool isPistol;
        private Transform rotatePoint;

        void Start()
        {
            rotatePoint = GetComponentsInChildren<Transform>()[1];
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

         void Update()
        {
            //вращаем оружие вокруг своей оси и перемещаем его вверх-вниз
            rotatePoint.localEulerAngles = new Vector3(rotatePoint.localEulerAngles.x, rotatePoint.localEulerAngles.y + Time.deltaTime * 50, rotatePoint.localEulerAngles.z);
            rotatePoint.position = new Vector3(rotatePoint.position.x,Mathf.PingPong(Time.time*0.1f , 0.3f)+0.15f, rotatePoint.position.z);
        }
    }
}
