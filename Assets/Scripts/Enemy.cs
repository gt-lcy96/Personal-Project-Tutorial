using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private Vector3 findPlayerPos;

    private float speed = 1.0f;


    public float maxHealth;
    public float health;
    public GameObject healthBarUI;
    public Slider slider;

    public Animator animator;
    void Start()
    {
        player = GameObject.Find("Player").gameObject;
        health = maxHealth;
        slider.direction = Slider.Direction.LeftToRight;
        slider.value = CalculateHealth();
  
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
        
    }


    void FixedUpdate()
    {
        
    }

    void UpdateHealthBar() 
    {
        slider.value = CalculateHealth();

        if(health < maxHealth)
        {
            healthBarUI.SetActive(true);
        }
        // destroy object even dmg overflow
        if(health <= 0) 
        {
            Destroy(gameObject);
        }

        // prevent healing over maxHealth
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }
    void MoveToPlayer()
    {
        findPlayerPos = (player.transform.position - transform.position).normalized;
        transform.LookAt(player.transform);
        transform.Translate(findPlayerPos * speed * Time.deltaTime, Space.World); 
    }
}
