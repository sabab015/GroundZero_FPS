using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bullet;

    public float rangeTotargetplayer, timebetweenShots = .5f;

    private float shotCounter;

    public Transform gun, firepoint;

    public float rotatespeed = 45f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeTotargetplayer)
        {
            gun.LookAt(PlayerController.instance.transform.position + new Vector3(0f, .5f, 0f));

            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                Instantiate(bullet, firepoint.position, firepoint.rotation);

                shotCounter = timebetweenShots;
            }
        }
        else
        {
            shotCounter = timebetweenShots;

            gun.rotation = Quaternion.Lerp(gun.rotation, Quaternion.Euler(0f, gun.rotation.eulerAngles.y + 5f, 0f), rotatespeed * Time.deltaTime);
        }
        
    }
}
