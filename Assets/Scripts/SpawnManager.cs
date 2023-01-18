using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemyPrefabs;
    void Start()
    {
        SpawnEnemy(0);
        SpawnEnemy(1);
        // SpawnEnemy(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy(int enemyIndex)
    {
        Instantiate(enemyPrefabs[enemyIndex], new Vector3(0, 0, 10), enemyPrefabs[enemyIndex].transform.rotation);
    }
}
