using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;


    public float moveSpeed, gravityModifier, jumpPower, runSpeed=12f;
    public CharacterController charCon;

    

    private Vector3 moveInput;

    public Transform camTrans;

    public int currentAmmo, maxAmmo;
    public float mouseSensitivity;

    public bool invertX;
    public bool invertY;
    
    private bool canJump, canDoubleJump;
    public Transform groundCheckpoint;
    public LayerMask whatIsGround;


    public Animator anim;

    
    public Transform firepoint;

    public Gun activeGun;

    public List<Gun> allGuns = new List<Gun>();

    public List<Gun> unlockableGuns = new List<Gun>();

    public int currentGun;

    public Transform adsPoint, gunholder;

    private Vector3 gunstartPosition;

    public float adsSpeed = 2f;

    public GameObject muzzleFlash;

    private float bounceAmount;
    private bool bounce;

    public float maxViewangle = 60f;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentAmmo = maxAmmo;
        UIController.instance.ammoSlider.maxValue = maxAmmo;
        UIController.instance.ammoSlider.value = currentAmmo;


        currentGun--;
        SwitchGun();

        gunstartPosition = gunholder.localPosition;
    }

    void Update()
    {
        if (!UIController.instance.pauseScreen.activeInHierarchy && !GameManager.instance.levelEnding)
        {
            // moveInput.x = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            // moveInput.z = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

            float yStore = moveInput.y;

            Vector3 vertMove = transform.forward * Input.GetAxis("Vertical");
            Vector3 horiMove = transform.right * Input.GetAxis("Horizontal");

            moveInput = vertMove + horiMove;
            moveInput.Normalize();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveInput = moveInput * runSpeed;
            }
            else
            {
                moveInput = moveInput * moveSpeed;
            }
            moveInput.y = yStore;

            moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

            if (charCon.isGrounded)
            {
                moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;

            }

            canJump = Physics.OverlapSphere(groundCheckpoint.position, .25f, whatIsGround).Length > 0;

            if (canJump)
            {
                canDoubleJump = false;
            }

            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                moveInput.y = jumpPower;

                canDoubleJump = true;

                AudioManager.instance.playSFX(8);
            }
            else if (canDoubleJump && Input.GetKeyDown(KeyCode.Space))
            {
                moveInput.y = jumpPower;

                canDoubleJump = false;

                AudioManager.instance.playSFX(8);

            }

            if(bounce)
            {
                bounce = false;
                moveInput.y = bounceAmount;

                canDoubleJump = true;

            }

            charCon.Move(moveInput * Time.deltaTime);


            Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

            if (invertX)
            {
                mouseInput.x = -mouseInput.x;
            }

            if (invertY)
            {
                mouseInput.y = -mouseInput.y;
            }


            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseInput.x, transform.rotation.eulerAngles.z);

            camTrans.rotation = Quaternion.Euler(camTrans.rotation.eulerAngles + new Vector3(mouseInput.y, 0f, 0f));

            muzzleFlash.SetActive(false);

            if (camTrans.rotation.eulerAngles.x > maxViewangle && camTrans.rotation.eulerAngles.x < 180f)
            {
                camTrans.rotation = Quaternion.Euler(maxViewangle, camTrans.rotation.eulerAngles.y, camTrans.rotation.eulerAngles.z);
            }
            else if(camTrans.rotation.eulerAngles.x > 180f && camTrans.rotation.eulerAngles.x < 360f - maxViewangle)

            {
                camTrans.rotation = Quaternion.Euler(-maxViewangle, camTrans.rotation.eulerAngles.y, camTrans.rotation.eulerAngles.z);

            }

            if (Input.GetMouseButtonDown(0) && activeGun.fireCounter <= 0)
            {
                RaycastHit hit;


                // firing shots 


                if (Physics.Raycast(camTrans.position, camTrans.forward, out hit, 50f))
                {
                    firepoint.LookAt(hit.point);

                }


                //Instantiate(bullet, firepoint.position, firepoint.rotation);

                fireShot();
            }

            if (Input.GetMouseButton(0) && activeGun.canAutoFire)
            {
                if (activeGun.fireCounter <= 0)
                {
                    fireShot();
                }

            }

            //gun switiching

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                SwitchGun();
            }

            if (Input.GetMouseButtonDown(1))
            {
                CameraController.instance.zoomIn(activeGun.zoomAmount);

            }

            if (Input.GetMouseButton(1))
            {
                gunholder.position = Vector3.MoveTowards(gunholder.position, adsPoint.position, adsSpeed * Time.deltaTime);
            }
            else
            {
                gunholder.localPosition = Vector3.MoveTowards(gunholder.localPosition, gunstartPosition, adsSpeed * Time.deltaTime);
            }

            if (Input.GetMouseButtonUp(1))
            {
                CameraController.instance.zoomOut();
            }
            anim.SetFloat("moveSpeed", moveInput.magnitude);
            anim.SetBool("onGround", canJump);

        }

    }

    public void fireShot()
    {
        if (activeGun.currentAmmo > 0)
        {
            activeGun.currentAmmo--;
            Instantiate(activeGun.bullet, firepoint.position, firepoint.rotation);
            UIController.instance.ammoSlider.value = currentAmmo;
            activeGun.fireCounter = activeGun.fireRate;
            UIController.instance.ammoText.text = "AMMO " + activeGun.currentAmmo;

            muzzleFlash.SetActive(true);
    

        }

      }

    public void SwitchGun()
    {

        activeGun.gameObject.SetActive(false);

        currentGun++;

        if(currentGun>= allGuns.Count)
        {
            currentGun = 0;
        }

        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);

        UIController.instance.ammoText.text = "AMMO " + activeGun.currentAmmo;

        firepoint.position = activeGun.firepoint.position;

    }

    public void AddGun(string gunToAdd)
    {
        bool gunUnlocked = false;

        if (unlockableGuns.Count >0 )
        {
            for(int i = 0; i < unlockableGuns.Count; i++)
            {
                if (unlockableGuns[i].gunName == gunToAdd)
                {
                    gunUnlocked = true;

                    allGuns.Add(unlockableGuns[i]);

                    unlockableGuns.RemoveAt(i);

                    i = unlockableGuns.Count;
                }
            }


        }

        if (gunUnlocked)
        {
            currentGun = allGuns.Count - 2;

            SwitchGun();
        }
    }

    public void Bounce(float bounceForce)
    {
        bounceAmount = bounceForce;
        bounce = true;
    }
}
