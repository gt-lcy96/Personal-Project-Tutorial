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

    public float speed = 4;
    float rotateSpeed = 40;

    void Start()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {

        // //turn the player to mouse position
        // Vector3 lookDirection = mousePos - transform.position;
        // Debug.Log("lookDirection :" + lookDirection);
        // // float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        // float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        // Debug.Log("angle :"+ angle);
        // Vector3 euler = new Vector3(0, angle, 0);
        // Quaternion quaternion = Quaternion.Euler(0, angle, 0);
        // //playerRb.rotation = quaternion

        // transform.Rotate(euler * rotateSpeed * Time.deltaTime);
        // transform.Rotate(euler);
    }

    void MovePlayer() 
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
        // moveVelocity = moveInput * moveSpeed;
        // transform.position += moveVelocity * Time.deltaTime;
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
    }
    void Update()
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

        MovePlayer();
        HandleAttack();
        
    }

    void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Shoot_b", true);  // set shooting animation
            Vector3 spawnPos = new Vector3(transform.position.x, 0.8f, transform.position.z + 0.5f);
            Instantiate(bulletPrefab, spawnPos, transform.rotation);
        } 
        else {
            //Idle Attack Animation Setting
            animator.SetInteger("WeaponType_int", 1);
            animator.SetBool("Shoot_b", false);
            animator.SetBool("Reload_b", false);
        }
    }
}
