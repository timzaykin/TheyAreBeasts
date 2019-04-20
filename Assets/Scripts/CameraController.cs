using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Vector3 position; //Смещение относительно якорной точки 

    public Transform CameraAncor; //Точка на которую будет смотреть камера 

    void Update()
    {
        if (CameraAncor !=null) { 
            transform.position = CameraAncor.position +  position;
            transform.LookAt(CameraAncor);
        }
    }
}
