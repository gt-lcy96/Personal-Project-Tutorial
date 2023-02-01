using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera cam;
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cam != null)
        {
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.back,
            cam.transform.rotation * Vector3.down);
            // transform.LookAt(transform.position + cam.transform.rotation * Vector3.back);
        }
    }
}
