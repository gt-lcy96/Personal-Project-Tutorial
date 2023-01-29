using System.Collections;
using UnityEngine;


public class Obstacle : MonoBehaviour, IDamageable
{
    
    private float health;
    public ObstacleScriptableObject obstacleScriptableObject;

    public virtual void SetupStats()
    {
        health = obstacleScriptableObject.health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}