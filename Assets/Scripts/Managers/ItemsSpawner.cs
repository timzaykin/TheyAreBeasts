using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAB { 
    public class ItemsSpawner : MonoBehaviour
    {
        public GameObject[] spwawnedItems;
        public GameObject[] spawnedWeapons;

        public float SpawnDelay = 5f;

        public void Start()
        {

            CalculateItemRoll(spwawnedItems);
            CalculateItemRoll(spawnedWeapons);
            StartCoroutine(SpawnItem());

        }

        private GameObject GetItemForSpawn(GameObject[] items ) {
            int randomValue = 0;
            foreach (GameObject item in items)
            {
                randomValue += item.GetComponent<ItemBase>().rareValue;
            }
            randomValue += (int)(randomValue * 1.35f);
            int randomResult = Random.Range(0, randomValue);

            Debug.Log(string.Format("random value:{0}, random result:{1}", randomValue, randomResult));
            for (int i = 0; i < items.Length; i++)
            {
                

                if (items[i].GetComponent<ItemBase>().ItemRoll <= randomResult)
                {
                    continue;
                }
                else
                {
                    Debug.Log(items[i].name);
                    return items[i];
                }
            }
            if (Random.Range(0, 2) > 0) {
                return GetItemForSpawn(spawnedWeapons);
            }
            else
            {
                Debug.Log("GO TO NEXT ROLL");
                return GetItemForSpawn(spwawnedItems);
            }
        }

        IEnumerator SpawnItem() {
            while (true)
            {
                yield return new WaitForSeconds(SpawnDelay);
                GameObject itemPref = GetItemForSpawn(spwawnedItems);
                Vector3 point = EnemyFactory.Instance.GetSpawnPoint(20);
                GameObject go = Instantiate(itemPref, point, Quaternion.identity) as GameObject;
                go.name = itemPref.name;

            }
        }

        private void CalculateItemRoll(GameObject[] colection) {
            for (int i = 0; i < colection.Length; i++)
            {
                if (i == 0) { colection[0].GetComponent<ItemBase>().ItemRoll = colection[0].GetComponent<ItemBase>().rareValue; }
                else
                {
                    int prevObjValue = 0;
                    for (int j = i-1; j >= 0; j--)
                    {
                        prevObjValue += colection[j].GetComponent<ItemBase>().rareValue;
                    }
                    colection[i].GetComponent<ItemBase>().ItemRoll = colection[i].GetComponent<ItemBase>().rareValue + prevObjValue;
                }
                Debug.Log(colection[i].GetComponent<ItemBase>().ItemRoll);
            }
        }
    }
}
