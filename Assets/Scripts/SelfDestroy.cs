using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour {
    //Уничтожение объекта через указанное время

    public float time;

    private void Start()
    {
        Destroy(gameObject, time);
    }

   
}
