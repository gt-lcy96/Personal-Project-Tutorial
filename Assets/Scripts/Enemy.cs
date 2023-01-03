using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private Vector3 findPlayerPos;

    public float speed = 4.0f;
    void Start()
    {
        player = GameObject.Find("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        findPlayerPos = (player.transform.position - transform.position).normalized;
        transform.Translate(findPlayerPos * speed * Time.deltaTime);
    }
}
