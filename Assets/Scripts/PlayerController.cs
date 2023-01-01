using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update\
    
    private Rigidbody playerRb;

    public float speed = 1;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
      
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(Vector3.right * horizontalInput * speed);
        playerRb.AddForce(Vector3.forward * verticalInput * speed);
        //transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        //transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);

    }
}
