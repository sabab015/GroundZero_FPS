using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{

    public string gun;

    // Start is called before the first frame update
    private bool collected;
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player" && !collected)
        {
            PlayerController.instance.AddGun(gun);
            Destroy(gameObject);

            collected = true;

            AudioManager.instance.playSFX(4);
        }
    }
}
