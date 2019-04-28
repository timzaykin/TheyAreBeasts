using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAB { 
    public class ItemBase : MonoBehaviour
    {

        public int rareValue;
        public int ItemRoll;

        private Transform rotatePoint;
        // Start is called before the first frame update

        protected virtual void Start()
        {
            rotatePoint = GetComponentsInChildren<Transform>()[1];
            Destroy(gameObject, Random.Range(20f, 40f));
        }


        // Update is called once per frame
        void Update()
        {
            //вращаем оружие вокруг своей оси и перемещаем его вверх-вниз
            rotatePoint.localEulerAngles = new Vector3(rotatePoint.localEulerAngles.x, rotatePoint.localEulerAngles.y + Time.deltaTime * 50, rotatePoint.localEulerAngles.z);
            rotatePoint.position = new Vector3(rotatePoint.position.x, Mathf.PingPong(Time.time * 0.1f, 0.3f) + 0.15f, rotatePoint.position.z);
        }
    }
}
