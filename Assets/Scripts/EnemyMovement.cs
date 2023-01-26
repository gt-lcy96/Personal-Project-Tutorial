using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public Transform Target;
    public float updateSpeed = 0.1f;
    private Animator animator;

    private NavMeshAgent Agent;
    void Awake()
    {
        
        Agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

    }

    void Start()
    {
        StartCoroutine(FollowTarget());
    }

    private IEnumerator FollowTarget()
    {
        
            while (enabled)
            {
                Agent.SetDestination(Target.transform.position);
                yield return new WaitForSeconds(updateSpeed);
            }
       
        
    }
    // Update is called once per frame
    void Update()
    {
        // make move animation based if Nav Agent is moving the target
        float animMoveSpeed = Agent.velocity.magnitude > 0.01f ? 0.5f : 0;
        animator.SetFloat("MoveSpeed",  animMoveSpeed);

    }
}
