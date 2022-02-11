using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public int damage = 25;

    public bool damageEnemy, damageplayer;
   
    

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Enemy" && damageEnemy)
        {
            //Destroy(other.gameObject);
            other.gameObject.GetComponent<EnemyHealthController>().DamageEnemy(damage);
        }

       

        if (other.gameObject.tag == "Player" && damageplayer)
        {
            Debug.Log("Hit the player!" + transform.position);
            PlayerHealthController.instance.DamagePlayer(damage);
        }

 
    }

}
