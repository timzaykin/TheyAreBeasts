using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAB { 
    public class ShotGun : Weapon
    {
        public int countFraction = 8;

        //Функция выстрела
        protected override void Shoot()
        {
            base.Shoot();

            for (int i = 0; i < countFraction-1; i++)
            {
                RaycastHit hit;
                var shootVector = new Vector3(Random.Range(scatter * -1, scatter), 0, 1);
                GameObject bullit = Instantiate(bullitPref, firePoint.position, Quaternion.LookRotation(transform.TransformDirection(shootVector))) as GameObject;
                bullit.GetComponent<Bullit>().bullitSpeed = bullitSpeed;
                if (Physics.Raycast(firePoint.position, firePoint.TransformDirection(shootVector), out hit, maxDistance))
                {
                    var damagable = hit.transform.gameObject.GetComponent<IDamagable>();
                    if (damagable != null)
                    {
                        var heading = hit.point - firePoint.position;
                        var distance = heading.magnitude;
                        var direction = heading / distance;
                        damagable.TakeDamage(damage, direction.normalized);
                    }
                }
            }
        }
    }
}
