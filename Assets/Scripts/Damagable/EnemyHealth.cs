using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAB { 
    public class EnemyHealth : Damagable {

        Animator anim; //Аниматор контроллер
        public int hitAnimCount; //Количество анимаций с попаданием

        public int score = 10;
        public override void Start()
        {
            base.Start();
            anim = GetComponent<Animator>();
        }

        public override void TakeDamage(int Damage, Vector3 hitDirection)
        {
            _hp -= Damage;
            if (_hp <= 0)
            {
                DestroyByHit(hitDirection);
            }
            else { 
                anim.SetInteger("AnimNumber",Random.Range(1, hitAnimCount+1));
                anim.SetTrigger("Hited");
                GetComponent<EnemyController>().Hitted();
            }
        }

        public override void DestroyByHit(Vector3 hitDirection)
        {
            gameObject.SetActive(false);
            GameObject ragdoll = Instantiate(diePref, transform.position, transform.rotation) as GameObject;
            ragdoll.GetComponentInChildren<Rigidbody>().AddForce(hitDirection * 100.0f, ForceMode.Impulse);
            EnemyFactory.Instance.enemyOnScene--;
            GameManager.Instance.score += score;
            //Тут Враг будет уходить в пул генерируемых объектов.
        }


    }
}
