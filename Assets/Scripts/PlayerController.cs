using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update\
    private Animator animator;
    private Rigidbody playerRb;
    public GameObject bulletPrefab;
    private Vector3 mousePos;

    public float moveSpeed = 5f;

    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private Camera mainCamera;
    private Transform firePoint;
    private SpawnManager spawnManager;

    public float speed = 4;

    void Start()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        firePoint = GameObject.Find("Fire Point").transform;
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    
    void Update()
    {
        LookAtCursor();
        MovePlayer();
        HandleAttack();
        HandleSpawnObstacle();
    }

    void LookAtCursor()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    void HandleSpawnObstacle()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 offset = new Vector3(0, -1, 0);
            Vector3 obstcaleSpawnPoint = new Vector3(firePoint.position.x, 1f, firePoint.position.z);
            spawnManager.SpawnObstacle(0, obstcaleSpawnPoint + offset, transform.rotation);
        }
    }
    void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Shoot_b", true);  // set shooting animation
            Vector3 spawnPos = new Vector3(transform.position.x, 0.8f, transform.position.z + 0.5f);
            
            Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        } 
        else {
            //Idle Attack Animation Setting
            animator.SetInteger("WeaponType_int", 1);
            animator.SetBool("Shoot_b", false);
            animator.SetBool("Reload_b", false);
        }
    }

    void MovePlayer() 
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime, Space.World);
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime, Space.World);
    }
}
