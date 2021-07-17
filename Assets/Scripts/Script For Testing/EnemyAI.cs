using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask WhatIsGround, WhatIsPlayer;

    //Patroling
    public Vector3 walkpoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    
    private void Awake() {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange)
            Patrolling();
        if(playerInSightRange && !playerInAttackRange)
            ChasePlayer();

    }

    private void Patrolling()
    {
        if(!walkPointSet)
            SearchWalkPoint();

        if(walkPointSet)
            agent.SetDestination(walkpoint);

        Vector3 distanceToWalkPoint = transform.position - walkpoint;

        //Walkpoint Reached
        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkpoint, -transform.up, 2f, WhatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        // transform.LookAt(player);
    }

}
