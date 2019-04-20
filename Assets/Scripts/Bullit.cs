using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullit : MonoBehaviour {
    public float bullitSpeed; //Скорость полета пули
    public GameObject impactPrefab; // Префаб эффекта попадания пули 
    private Vector3 previousPosition; // Позиция в предидущем вызове FixedUpdate
    public LayerMask mask; // Маска слоя

    public void Start()
    {
        previousPosition = GameObject.Find("FirePoint").transform.position;
    }

    private void Update()
    {
        //Передвижение пули кажждый кадр
        transform.position += transform.TransformDirection(Vector3.forward) * bullitSpeed * Time.deltaTime;
        CheckCollision(previousPosition);
        //заменяем предыдущую позицию позицией в текущем вызове
        previousPosition = transform.position;
    }

    private void FixedUpdate()
    {
        //transform.position += transform.TransformDirection(Vector3.forward) * bullitSpeed * Time.deltaTime;
        //CheckCollision(previousPosition);
        ////заменяем предыдущую позицию позицией в текущем вызове
        //previousPosition = transform.position;

    }

    //Проверка коллизии между текущей позицией и пердыдущей позицией
    void CheckCollision(Vector3 prevPos)
    {
        RaycastHit hit;
        Vector3 direction = transform.position - prevPos;
        Ray ray = new Ray(prevPos, direction);
        float dist = Vector3.Distance(transform.position, prevPos);
        if (Physics.Raycast(ray, out hit, dist, mask))
        {

            if (hit.transform.gameObject.tag != "CurrentPlayer") {
                transform.position = hit.point;
                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                Vector3 pos = hit.point;
                Destroy(gameObject);
                Instantiate(impactPrefab, pos, rot);
            }
        }
    }


    ////Функция на будущее
    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag != "FX")
    //    {
    //        ContactPoint contact = collision.contacts[0];
    //        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, contact.normal);
    //        Vector3 pos = contact.point;
    //        Instantiate(impactPrefab, pos, rot);

    //    }
    //}
}
