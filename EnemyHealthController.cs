﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{

    public int currentHealth = 5;

    public EnemyController theEC;
    
  
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void DamageEnemy(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (theEC != null)
        {
            theEC.GetShot();
        }

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            AudioManager.instance.playSFX(2);
        }
    }
}
