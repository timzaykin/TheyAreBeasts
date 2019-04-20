using System.Collections;
using UnityEngine;

namespace TAB
{
    public class Weapon : MonoBehaviour
    {
        public Transform firePoint; //точка из которой производятся выстрелы
        public GameObject muzzleFlash;
        public GameObject bullitPref;
        [SerializeField]
        private int _ammo; //текущее количество патронов
        private int Ammo { get { return _ammo; }}
        public int maxAmmo; //Максимальное количество патронов в обойме
        [SerializeField]
        private int _allAmmo; // Количество патронов в запасе
        private int AllAmmo { get { return _allAmmo; } }
        public int maxAllAmmo; // Максимальное количество патронов в запасе
        public bool infinityAmmo; //Бесконечные патроны
        public int damage; //Наносимый урон
        public float firerateDelay; //задержка между выстралми
        public float reloadDelay; //задержка перезарядки
        public float maxDistance; //максимальная дальность стрельбы
        public float scatter; //показатель разброса стрельбы от 0 до 0,3
        public float bullitSpeed; // скорость анимации пули
        public WeaponManger.WeaponAnimation WeaponAnimation;//Анимация текущего оружия

        private bool reloading; // Флаг перезорядки оружия
        private bool shootDelay; //Флаг задержки выстрела

        private void OnDisable()
        {
            shootDelay = false;
            reloading = false;
        }

        private void Update()
        {
            // Временная реализация выстрелов, в дальнейшем надо переделать с использованием Events
            if (InputController.Instance.isFire)
            {
                if (_ammo > 0 && !shootDelay)
                {
                    StartCoroutine(SingleShootCourotine());
                }
                if (_ammo <= 0 && !reloading)
                {
                    StartCoroutine(Reload());
                }
            }


        }


        //Функция выстрела
        protected virtual void Shoot()
        {
            _ammo--;
            StartCoroutine(MuzzleFlashCorotine());
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
            else
            {
                //Debug.Log("Not Hit!");
            }
        }

        //Куротина для вспышки
        IEnumerator MuzzleFlashCorotine()
        {
            muzzleFlash.SetActive(false);
            yield return new WaitForSeconds(0.001f);
            muzzleFlash.SetActive(true);
        }

        //Куротина для перезарядки
        IEnumerator Reload()
        {
            reloading = true;
            yield return new WaitForSeconds(reloadDelay);
            if (!infinityAmmo)
            {
                if (_allAmmo > 0)
                {
                    if (_allAmmo > maxAmmo)
                    {
                        _ammo = maxAmmo;
                        _allAmmo -= maxAmmo;
                    }
                    else
                    {
                        _ammo = _allAmmo;
                        _allAmmo = 0;
                    }
                }
            }
            else
            {
                _ammo = maxAmmo;
            }
            reloading = false;
        }

        //Куротина задержки выстрелов
        IEnumerator SingleShootCourotine()
        {
            shootDelay = true;
            Shoot();
            yield return new WaitForSeconds(firerateDelay);
            shootDelay = false;
        }

        public void AddAmmo(int ammo) {
            _allAmmo += ammo;
        }
    }
}
