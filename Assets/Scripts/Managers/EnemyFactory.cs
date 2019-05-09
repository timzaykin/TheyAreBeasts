using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace TAB
{
    public class EnemyFactory : Singleton<EnemyFactory>
    {

        private int enemyCount; // количесво созданных врагов
        public int enemyOnScene; //Текущее количество на сцене
        public int maxEnemyOnScene; // Максимальное количество на сцене
        public float spawnDelay; //Задержка спавна 
        public float minSpawnDelay; //Минимальная задержка
        public GameObject[] HordeEnemys; // Префабы врагов
        public GameObject[] EliteEnemys; // Префабы крутых врагов
        private void OnEnable()
        {
            GameManager.Instance.onStart += StartSpawner; //Подписываемся на событие старта игры
        }

        private void OnDisable()
        {
            GameManager.Instance.onStart -= StartSpawner;//ОТписываемся от этого же события
        }

        //Запускаем куратину спавнера
        public void StartSpawner() {
            Debug.Log("Spawner is Started");
            StartCoroutine("SpawnTimer");
        }

        //Спавним врагов, постепенно уменьшая время спавна до минимального
        IEnumerator SpawnTimer()
        {
            Debug.Log("Spawner is Worked");

            do
            {

                if (spawnDelay > minSpawnDelay)
                {
                    spawnDelay -= enemyCount * 0.002f;
                }
                yield return new WaitForSeconds(spawnDelay);
                if (enemyOnScene < maxEnemyOnScene)
                {
                    GameObject enemyPref = GetEnemy();
                    SpawnEnemy(enemyPref);
                }
                else
                {
                    continue;
                }

            } while (GameManager.Instance.CurrentGameState == GameManager.GameState.Started);


        }

        //Выбираем какого врага надо сгенерировать в этой итеррации
        private GameObject GetEnemy()
        {
            if (enemyCount != 0 && enemyCount % 20 == 0)
            {
                return EliteEnemys[UnityEngine.Random.Range(0, EliteEnemys.Length)];
            }
            else { 
            return HordeEnemys[UnityEngine.Random.Range(0,HordeEnemys.Length)];
            }
        }
        //Функция генерирования врагов, в дальшейшем враги будут не создаваться а перетаскиваться из пула
        public void SpawnEnemy(GameObject enemyPrefab)
        {
            Vector3 point = GetSpawnPoint(38.0f);
            GameObject Enemy = Instantiate(enemyPrefab, point, Quaternion.identity) as GameObject;
            enemyCount++;
            enemyOnScene++;
            Enemy.name = enemyPrefab.name;
            Enemy.transform.parent = GameObject.Find("DynamicObjects").transform;
        }


        //Получаем точку на нав меше, для спавна врага
        public Vector3 GetSpawnPoint( float range)
        {
            Vector3 rapnomPosition = new Vector3(UnityEngine.Random.Range(-1*range, range), 0, UnityEngine.Random.Range(-1 * range, range));
            Vector3 point;
            while (!RandomPoint(rapnomPosition, 3f, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
            }
            return point;
        }

        bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {
            for (int i = 0; i < 30; i++)
            {
                Vector3 randomPoint = center + UnityEngine.Random.insideUnitSphere * range;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
                {
                    result = hit.position;
                    return true;
                }
            }
            result = Vector3.zero;
            return false;
        }


    }
}