using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    private Transform Target;
    public float updateSpeed = 0.1f;
    private Animator animator;

    private NavMeshAgent Agent;
    private bool hasObstacleNearby = false;

    void Awake()
    {
        
        Agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Target = GameObject.Find("Player").GetComponent<Transform>();

    }

    void Start()
    {
        StartCoroutine(FollowTarget());
    }

    private IEnumerator FollowTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(updateSpeed);
            while (enabled)
            {
                if(!hasObstacleNearby) {
                    Agent.SetDestination(Target.transform.position);
                    yield return wait;
                } else {
                    
                }
                
            }
       
        
    }
    // Update is called once per frame
    void Update()
    {
        // make move animation based if Nav Agent is moving the target
        float animMoveSpeed = Agent.velocity.magnitude > 0.01f ? 0.5f : 0;
        animator.SetFloat("MoveSpeed",  animMoveSpeed);

    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Obstacle")) {
            hasObstacleNearby = true;
        }
    }

    private void AttackNearbyTarget()
    {
        // attackAnim.Play();
        // target.dealDamage();   
    }
}
