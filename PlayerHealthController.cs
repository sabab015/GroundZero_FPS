using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{ 
    public static PlayerHealthController instance;

    public int maxHealth, currentHealth;

    public float invincibleLength = 1f;
    private float invincibleCounter;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = "HEALTH : " + currentHealth + "/" + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter >0)
        {
            invincibleCounter -= Time.deltaTime;
        }
    }

    public void DamagePlayer (int damageAmount)
    {

        if (invincibleCounter <= 0 && !GameManager.instance.levelEnding)
        {
            AudioManager.instance.playSFX(7);
            currentHealth -= damageAmount;

            UIController.instance.showDamage(); 

            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);

                currentHealth = 0;

                GameManager.instance.playerDied();

                AudioManager.instance.stopbgm();
                AudioManager.instance.playSFX(6);
                AudioManager.instance.stopSFX(7);

            }
            invincibleCounter = invincibleLength;

            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = "HEALTH : " + currentHealth + "/" + maxHealth;
        }
        }


    public void HealPlayer(int healAmount)

    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;

        }
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = "HEALTH : " + currentHealth + "/" + maxHealth;
    }

}
