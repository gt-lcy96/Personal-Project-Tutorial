using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemyPrefabs;
    private int enemyCount = 0;
    void Start()
    {
        //SpawnEnemy(0);
        //SpawnEnemy(1);
        StartCoroutine(SpawnEnemyWave());
        // SpawnEnemy(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyWave()
    {
        
        while(enemyCount < 15)
        {
            yield return new WaitForSeconds(0.5f);
            SpawnEnemy(Random.Range(0, 2));
            enemyCount++;
        }
    }
    void SpawnEnemy(int enemyIndex)
    {
        Instantiate(enemyPrefabs[enemyIndex], new Vector3(0, 0, 10), enemyPrefabs[enemyIndex].transform.rotation);
    }
}
