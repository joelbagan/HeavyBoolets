using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    //Ammo
    public int startingAmmoCap;     //initial ammo cap
    public int startingLoadedAmmo;  //initial ammo in gun
    public int startingReload;      //initial reload
    public int startingHoldingAmmo; //initial ammo held
    private int ammoCap;             //maximum number of Boolets that the gun can hold
    private int holdingAmmo;         //amount of ammo held but not in gun
    private int loadedAmmo;         //number of boolets in gun
    public int reloadAmount;       //amount reloaded on each Reload()
    public Material ammoActive;     
    public Material ammoInactive;
    public GameObject[] AmmoIndicators;

    private Camera playerCamera;

    //Shooting
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public GameObject Boolet;
    public float offsetMagnitude;   //magnitude of vector drawn back from surface hit by shootRay used to spawn Boolet to avoid falling through walls
    public float reboundMagnitude;  //magnitude of force vector applied to boolet on instantiation after shooting to simulate ricochet 
    public AudioClip shootClip;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    AudioSource gunAudio;

    void Awake()
    {
        gunAudio = GetComponentInChildren<AudioSource>();
        playerCamera = GetComponentInChildren<Camera>();
        loadedAmmo = startingLoadedAmmo;
        ammoCap = startingAmmoCap;
        reloadAmount = startingReload;
        holdingAmmo = startingHoldingAmmo;
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (loadedAmmo > 0 && Input.GetButtonDown("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
            DecrementAmmoCount();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (holdingAmmo > 0 && loadedAmmo < ammoCap) //only try if player has held ammo and not already at max
            {                
                Reload();
            }
        }
    }

    void DecrementAmmoCount()
    {
        AmmoIndicators[loadedAmmo - 1].GetComponent<Renderer>().material = ammoInactive;
        --loadedAmmo;
    }

    void IncrementAmmoCount()
    {
        AmmoIndicators[loadedAmmo].GetComponent<Renderer>().material = ammoActive;
        ++loadedAmmo;
    }

    public void IncrementHoldingAmmoCount()
    {
        ++holdingAmmo;
    }

    public void DecrementHoldingAmmoCount()
    {
        --holdingAmmo;
    }

    //Reloads up to reloadAmount boolets, or until maxAmmo is reached or holdingAmmo is 0
    void Reload()
    {
        for (int i = 0; i < reloadAmount; ++i)
        {
            if (loadedAmmo == ammoCap || holdingAmmo == 0)
            {
                return;
            }
            else
            {
                IncrementAmmoCount();
                DecrementHoldingAmmoCount();
            }
        }
    }

    /*
     * Raycasts in direction of camera until range or until hit
     * on hit, spawns boolet offset from point of hit
     */
    void Shoot()
    {
        gunAudio.clip = shootClip;
        gunAudio.Play ();
        timer = 0f;


        shootRay.origin = playerCamera.transform.position;
        shootRay.direction = playerCamera.transform.forward;

        if (Physics.Raycast(shootRay, out shootHit))
        {
            Vector3 bulletSpawn = shootHit.point - shootRay.direction * offsetMagnitude;
            GameObject boolet = Instantiate(Boolet, bulletSpawn, playerCamera.transform.rotation);
            boolet.GetComponent<Rigidbody>().AddForce(shootHit.normal.normalized * reboundMagnitude);
            boolet.GetComponent<Rigidbody>().AddForce(new Vector3(0,1,0) * reboundMagnitude);

            if (shootHit.collider.CompareTag("Enemy"))
            {
                shootHit.collider.GetComponent<EnemyHealth>().TakeDamage();
            }
            if(shootHit.collider.CompareTag("Turret Battery"))
            {
                shootHit.collider.GetComponent<TurretHealth>().TakeDamage();
            }
            //TODO: add some rebound force to boolet on spawn
        }
        
    }

    
}

