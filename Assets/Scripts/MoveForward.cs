using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        DestroyOutOfBound();
    }

    void DestroyOutOfBound()
    {
        float xBound = 100;
        float zBound = 100;
        if (transform.position.x > xBound || transform.position.x < -xBound)
        {
            Destroy(gameObject);
        } else if (transform.position.z > zBound || transform.position.z < -zBound)
        {
            Destroy(gameObject);
        }

    }
}
