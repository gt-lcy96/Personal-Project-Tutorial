using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameManager gameManager;
    public float bulletDmg = 25;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager")?.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            DealDamage(other.gameObject.GetComponent<Enemy>(), bulletDmg);
            // Destroy(other.gameObject);
            if(gameManager != null) 
            {
                gameManager.killCount++;
            }
        }
    }

    private void DealDamage(Enemy enemy, float damage)
    {
        enemy.health -= damage;
    }
}
