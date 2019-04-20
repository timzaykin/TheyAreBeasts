using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAB { 
    public class PlayerHealth : Damagable {
        [SerializeField]
        private int armour; //Текущий показатель брони
        public int maxArmour; // Максимальный показатель брони

        public override void DestroyByHit()
        {
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<Animator>().SetBool("Death", true);
            // Тут нужно запускать Фуникцию окончания игры
        }

        //Получение урона игрока(С учетом брони)
        public override void TakeDamage(int Damage)
        {
            if (armour > 0) {
                armour -= Damage;
                if (armour < 0)
                {
                    Damage = armour;
                    armour = 0;
                }
                else
                {
                    Damage = 0;
                }
            }
 
            Hp -= Damage;
            if (Hp <= 0)
            {
                DestroyByHit();
            }
        }

        //Получение брони
        public void TakeArmour(int AddedArmour)
        {
            if ((armour + AddedArmour) > maxArmour)
            {
                armour = maxArmour;
            }
            else
            {
                armour += AddedArmour;
            }
        }
    }
}
