using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable  {

    int Hp{ get; set; }

    void TakeDamage(int Damage);
    void TakeDamage(int Damage, Vector3 hitDirection);
    void TakeHeal(int Heal);
    void DestroyByHit();
    void DestroyByHit(Vector3 hitDirection);
}
