using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public Transform target;
    public float updateSpeed = 0.1f;
    [SerializeField]
    private Animator animator;

    private NavMeshAgent Agent;
    public bool hasObstacleNearby = false;

    void Awake()
    {
        
        Agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player").GetComponent<Transform>();

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

                Agent.SetDestination(target.transform.position);                
                if(hasObstacleNearby)
                {
                    AttackNearbyTarget();
                }
                yield return wait;
                
            }
       
        
    }
    // Update is called once per frame
    void Update()
    {
        // make move animation based if Nav Agent is moving the target
        float animMoveSpeed = Agent.velocity.magnitude > 0.01f ? 0.5f : 0;
        animator.SetFloat("MoveSpeed",  animMoveSpeed);

    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.CompareTag("Obstacle")) {
            hasObstacleNearby = true;
            // Target = other.gameObject.transform;
        }
    }

    public void AttackNearbyTarget()
    {
        // attackAnim.Play();
        animator.SetTrigger("Attack");

        // target.dealDamage();   
    }
}
