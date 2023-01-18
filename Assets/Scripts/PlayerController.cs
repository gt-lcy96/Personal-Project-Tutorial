using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update\
    private Animator animator;
    private Rigidbody playerRb;
    public GameObject bulletPrefab;
    

    public float speed = 4;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        HandleAttack();
    }

    // Move Player by arrow keys
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
    }

    void HandleAttack()
    {

        
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Attack");
            animator.SetBool("Shoot_b", true);  // set shooting animation
            Vector3 spawnPos = new Vector3(transform.position.x, 0.8f, transform.position.z + 0.5f);
            Instantiate(bulletPrefab, spawnPos, transform.rotation);
        } 
        else {
            IdleAttackAnim()
        }
    }

    void IdleAttackAnim() 
    {
        animator.SetInteger("WeaponType_int", 1);
        animator.SetBool("Shoot_b", false);
        animator.SetBool("Reload_b", false);

    }    

    void ShootingAnim()
    {
        
    }
}
