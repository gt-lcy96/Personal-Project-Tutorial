using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public Transform Target;
    public float updateSpeed = 0.1f;

    private NavMeshAgent Agent;
    void Awake()
    {
        
        Agent = GetComponent<NavMeshAgent>();
        

    }

    void Start()
    {
        StartCoroutine(FollowTarget());
    }

    private IEnumerator FollowTarget()
    {
        
            while (enabled)
            {
                try
                {
                    Agent.SetDestination(Target.transform.position);
                 }
                 catch
                {
                    Debug.Log("Catch Error occurred.");
                }
                yield return new WaitForSeconds(updateSpeed);
            }
       
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
