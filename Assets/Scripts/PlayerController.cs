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
    private CameraShake cameraShake;
    public float speed = 4;
    private PlayerInteraction playerInteract;

    void Start()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
        firePoint = GameObject.Find("Fire Point").transform;
        spawnManager = GameObject.Find("Spawn Manager")?.GetComponent<SpawnManager>();
        cameraShake = mainCamera.GetComponent<CameraShake>();
        playerInteract = GetComponentInChildren<PlayerInteraction>();
    }

    
    void Update()
    {
        LookAtCursor();
        MovePlayer();
        HandleAttack();
        HandleSpawnObstacle();
        HandleInteract();
    }

    void HandleInteract()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E pressed");
            playerInteract.Interact();
        }
    }

    void LookAtCursor()
    {
        RaycastHit _hit;
        Ray _ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit))
        {
            transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z));
        }
    }

    void HandleSpawnObstacle()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Vector3 offset = new Vector3(0, -1, 0);
            Vector3 obstcaleSpawnPoint = new Vector3(firePoint.position.x, 1f, firePoint.position.z);
            if(spawnManager != null)
            {
                spawnManager.SpawnObstacle(0, obstcaleSpawnPoint + offset, transform.rotation);
            }
        }
    }
    void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Shoot_b", true);  // set shooting animation
            Vector3 spawnPos = new Vector3(transform.position.x, 0.8f, transform.position.z + 0.5f);
            
            Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            cameraShake.TriggerShake(.05f, .1f);

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
