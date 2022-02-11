using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    
    public float moveSpeed, lifetime;
    public Rigidbody theRB;
    public GameObject impactEffect;

    public int damage=1;

    public bool damageEnemy, damageplayer;
    void Start()
    {
        
    }
    void Update()
    {
        theRB.velocity = transform.forward * moveSpeed;
        lifetime -= Time.deltaTime;

        if(lifetime<=0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag== "Enemy" && damageEnemy)
        {
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
        }

        if(other.gameObject.tag == "Headshot" && damageEnemy )
        {
            other.transform.parent.GetComponent<EnemyHealthController>().DamageEnemy(damage * 5);
        }

        if(other.gameObject.tag == "Player" && damageplayer)
        {
            Debug.Log("Hit the player!" + transform.position);
            PlayerHealthController.instance.DamagePlayer(damage);
        }

        Destroy(gameObject);


        Instantiate(impactEffect, transform.position +(transform.forward * (-moveSpeed * Time.deltaTime)), transform.rotation);
    }

  
}
