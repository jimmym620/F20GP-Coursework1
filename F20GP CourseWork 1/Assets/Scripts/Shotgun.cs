
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Extended from Brackeys' Raycast shooting tutorial https://youtu.be/THnivyG0Mvo?list=RDCMUCYbK_tjZ2OrIZFBvU6CCMiA
// and Ammo + reloading tutorial https://youtu.be/kAx5g9V5bcM
public class Shotgun : MonoBehaviour
{
    public float damage = 45f;
    public float range = 40f;
    public float fireRate = 2f;
    float origFireRate;
    float boostedFireRate;
    public float impactForce = 55f;

    public int maxAmmo = 7;
    public Text ammoDisplay;
    public int currentAmmo;
    public float reloadTime = 3f;
    float origReloadTime;
    float boostedReloadTime;
    private bool isReloading = false;

    // --- A second camera is used with a mask to prevent weapons clipping into other objects

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    public Animator animator;
    public AudioSource shootSound;
    public AudioSource reloadSound;

    void Start() 
    {
        currentAmmo = maxAmmo;
        origFireRate = fireRate;
        boostedFireRate = fireRate * 2;

        origReloadTime = reloadTime;
        boostedReloadTime = reloadTime / 2;

    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    void Update()
    {
        // --- Ammo counter UI
        ammoDisplay.text = "Shotgun \n" + currentAmmo.ToString() + "/" + maxAmmo;
        
        if (isReloading)
            return;

        // If gun is empty, reload the gun
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        // Calculation for fire rate (time between each shot)
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            Shoot();
        }

        // --- If R is pressed, reload the gun
        if (Input.GetKey("r"))
        {
            StartCoroutine(Reload());
            return;
        }

        // ---  If weapon boost is picked up, start the coroutine, if the boost is over, stop the coroutine
        if(UI_Manager.instance.WeaponBoostActive == true)
        {
            StartCoroutine("WeaponBoosted");
        }

        if (UI_Manager.instance.WeaponBoostActive == false)
        {
            StopCoroutine("WeaponBoosted");
        }

    }

    // Play reload anim + sound, wait for the length of reloadTime, go back to the default animation, fill the ammo count back up
    IEnumerator Reload()
    {
        isReloading = true;
        

        animator.SetBool("Reloading", true);
        reloadSound.Play();

        yield return new WaitForSeconds(reloadTime);

        animator.SetBool("Reloading", false);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    // Plays sound and particle effect when shooting
    // Raycasting to detect and hit targets - From Brackeys
    void Shoot()
    {
        // --- Plays sound and particle effect when shooting
        muzzleFlash.Play();
        shootSound.Play();

        currentAmmo--;
        RaycastHit Hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out Hit, range))
        {
            // Targets take damage and are hit with a force
            Targets target =Hit.transform.GetComponent<Targets>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (Hit.rigidbody != null)
            {
                Hit.rigidbody.AddForce(Hit.normal * -impactForce);
            }
           GameObject impactGO = Instantiate(impactEffect, Hit.point, Quaternion.LookRotation(Hit.normal));
            Destroy(impactGO, 3f);
        }
    }

    // --- Coroutine for Weapon Boost (2x fire rate, reload speed cut in half)
    IEnumerator WeaponBoosted()
    {
        fireRate = boostedFireRate;
        reloadTime = boostedReloadTime;
        yield return new WaitForSeconds(7);
        fireRate = origFireRate;
        reloadTime = origReloadTime;

        UI_Manager.instance.WeaponBoostActive = false;
    }
}
