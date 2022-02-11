using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    
    private bool chasing;
    public float distanceToChase = 10f, distanceToLose = 15f, distanceToStop = 5f;

    private Vector3 targetPoint, initialPoint;

    public NavMeshAgent agent;

    public float keepChasingTime = 5f;

    public float chaseCounter;

    public GameObject bullet;

    public Transform firePoint;

    public float fireRate, waitBetweenShots = 2f, timeToShoot = 1f;

    private float fireCount, shotWaitCounter, shootTimeCounter;

    public Animator anim;

    private bool wasShot;
    
    void Start()
    {
        initialPoint = transform.position;

        shootTimeCounter = timeToShoot;

        shotWaitCounter = waitBetweenShots; 
    }

    
    void Update()
    {
        targetPoint = PlayerController.instance.transform.position; // enemy AI will calculate player's position.
       
        if (!chasing)
        {
            if (Vector3.Distance(transform.position, targetPoint) < distanceToChase)
            {
                chasing = true;

                shootTimeCounter = timeToShoot;

                shotWaitCounter = waitBetweenShots;
            }

            if (chaseCounter > 0)
            {
                chaseCounter -= Time.deltaTime;
                

                if (chaseCounter <= 0)
                {
                    
                    agent.destination = initialPoint;
                }
            }

            if(agent.remainingDistance < .25f)
            {
                anim.SetBool("isMoving", false);
            }
            
            else
            {
                anim.SetBool("isMoving", true);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, targetPoint) > distanceToStop)
            {

                agent.destination = targetPoint;
            }
            else
            {
                agent.destination = transform.position;

            }

            if (Vector3.Distance(transform.position, targetPoint) > distanceToLose)
            {
                if (!wasShot)
                {
                    chasing = false;


                    agent.destination = initialPoint;

                    chaseCounter = keepChasingTime;
                }
            }

            else
            {
                wasShot = false;
            }

            if (shotWaitCounter > 0)

            {
                shotWaitCounter -= Time.deltaTime;
                if (shotWaitCounter <=0)

                {
                    shootTimeCounter = timeToShoot;
                }
                anim.SetBool("isMoving", true);
            }
            else
            {
                if (PlayerController.instance.gameObject.activeInHierarchy)
                {

                    shootTimeCounter -= Time.deltaTime;

                    if (shootTimeCounter > 0)
                    {
                        fireCount -= Time.deltaTime;
                        if (fireCount <= 0)

                        {
                            fireCount = fireRate;

                            firePoint.LookAt(PlayerController.instance.transform.position + new Vector3(0f, 0.365f, 0f));

                            Vector3 targetDir = PlayerController.instance.transform.position - transform.position;

                            float angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);
                            if (Mathf.Abs(angle) < 30f)
                            {

                                Instantiate(bullet, firePoint.position, firePoint.rotation);

                                anim.SetTrigger("fireShot");
                            }

                            else
                            {
                                shotWaitCounter = waitBetweenShots;
                            }
                        }
                    }
                    else
                    {
                        shotWaitCounter = waitBetweenShots;
                    }

                }
                anim.SetBool("isMoving", false);
            }
        }

    }

    public void GetShot()
    {
        wasShot = true;

        chasing = true;

        

    }

    public void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Ground"))
        {
            agent.enabled = false;
            agent.enabled = true;
            
        }
    }
}

