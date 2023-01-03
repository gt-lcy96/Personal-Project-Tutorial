using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update\
    
    private Rigidbody playerRb;

    public float speed = 4;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        MovePlayer();
    }

    // Move Player by arrow keys
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
    }
}
