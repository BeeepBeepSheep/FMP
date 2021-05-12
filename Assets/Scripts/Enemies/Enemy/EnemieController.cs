﻿using UnityEngine;
using UnityEngine.AI;

public class EnemieController : MonoBehaviour
{
    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;
    public GameObject attackField;
    public Animator animController;
    //----------
    public GameObject head;
    private GameObject playerCapsual;
    public float laserRange = 100f;
    public float damage = 1f;
    public float laserFireRate;
    private float nextTimeToFire;
    private LineRenderer lineRenderer;

    public bool canReachTarget;
    public bool canSeeTarget;
    void Start()
    {
        target = PlayerManeger.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();

        lineRenderer = head.GetComponent<LineRenderer>();
        playerCapsual = GameObject.FindGameObjectWithTag("PlayerCapsual");

    }

    void Update()
    {
        Wallrun player = target.GetComponent<Wallrun>();

        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            if (player.isWallRunning2 == false)
            {
                // Chasing state
                animController.SetInteger("State", 3);

                NavMeshPath path = new NavMeshPath();
                agent.CalculatePath(target.position, path);

                if (path.status == NavMeshPathStatus.PathPartial)
                {
                    //Unreachable state
                    canReachTarget = false;
                    TargetIsUnreachable_State();
                }
                if (path.status != NavMeshPathStatus.PathPartial)
                {
                    head.SetActive(false);
                    canReachTarget = true;
                    agent.SetDestination(target.position);

                    if (distance <= agent.stoppingDistance)
                    {
                        // Attacking state
                        FaceTarget();
                        Punching_State();
                    }
                }
                else
                {
                    animController.SetInteger("State", 1);
                    //Debug.Log("idle");
                }
            }
            else
            {
                FaceTarget();
                Turret_State();
            }

        }
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    void Punching_State()
    {
        //Debug.Log("attacking");
        animController.SetInteger("State", 2);
    }
    void Turret_State()
    {
        //Debug.Log("turret");
        animController.SetInteger("State", 1);

        agent.SetDestination(transform.position);

        Vector3 direcetion = target.position - head.transform.position;
        Quaternion rotation = Quaternion.LookRotation(direcetion);
        head.transform.rotation = rotation;

        head.SetActive(true);


        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / laserFireRate;

            RaycastHit hit;
            if (Physics.Raycast(head.transform.position, head.transform.forward, out hit, laserRange))
            {
                //Debug.Log(hit.transform.tag);
                if(hit.collider)
                {
                    lineRenderer.SetPosition(1, new Vector3(0, 0, hit.distance));
                    
                    if (hit.transform.tag == "Player")
                    {
                        Health player1 = playerCapsual.GetComponent<Health>();
                        player1.TakeDamage(damage);
                    }
                }
                else
                {
                    lineRenderer.SetPosition(1, new Vector3(0, 0, laserRange));
                }
            }
        }
    }
    void TargetIsUnreachable_State()
    {
        FaceTarget();
        RaycastHit hit;
        if (Physics.Linecast(head.transform.position, target.position, out hit))
        {
            if (hit.transform.name == "Player")
            {
                canSeeTarget = true;
                Turret_State();
            }
            else
            {
                head.SetActive(false);
                canSeeTarget = false;
                // Chasing state
                animController.SetInteger("State", 3);
                agent.SetDestination(target.position);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, lookRadius);
    }
}