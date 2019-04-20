using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour, IDamagable {

    public GameObject diePref; //Префаб разрушенного объекта
    public int maxHp;  //Максимальное количество хп
    [SerializeField]
    protected int _hp; //Текущее количество хп
    public int Hp
    {
        get { return _hp; }
        set { _hp = value; }
    }

    public virtual void Start()
    {
        _hp = maxHp;
    }

    //разрушение объекта
    public virtual void DestroyByHit()
    {
        Destroy(gameObject);
        Instantiate(diePref, transform.position, transform.rotation);
    }

    public virtual void DestroyByHit(Vector3 hitDirection)
    {
        DestroyByHit();
    }

    //Получение урона
    public virtual void TakeDamage(int Damage)
    {
        _hp -= Damage;
        if (_hp <= 0) {
            DestroyByHit();
        }

        print(gameObject.name + " hp:" + _hp);
    }
    public virtual void TakeDamage(int Damage, Vector3 hitDirection) {
        TakeDamage(Damage);
    }

    //Лечение
    public void TakeHeal(int Heal)
    {
        if ((_hp + Heal) > maxHp) {
            _hp = maxHp;
        }
        else {
            _hp += Heal;
        }
    }



}
