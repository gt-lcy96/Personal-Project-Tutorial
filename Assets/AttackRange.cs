using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField]
    private EnemyMovement enemyMovement;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("OBSTACLE TRIGGER");
            
            enemyMovement.hasObstacleNearby = true;
            enemyMovement.target = other.gameObject.transform;
            // enemyMovement.AttackNearbyTarget(other.gameObject.transform);
        }
    }
}
