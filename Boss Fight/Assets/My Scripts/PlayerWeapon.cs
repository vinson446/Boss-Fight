using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public float currentClipSize;
    public float clipSize = 30f;
    public float totalBullets = 120f;

    public Camera fpsCam;

    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float impactDuration;

    private float nextTimeToFire = 0f;

    private AudioSource audioSource;
    public AudioClip[] gunFire;

    public GunRecoil gunRecoil;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();

        currentClipSize = clipSize;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire && currentClipSize > 0)
        {
            // greater the fireRate, faster it is
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            float amountToReload;

            //animator.Play("Reload");
            animator.Play("Reload", -1, 0);

            audioSource.pitch = Random.Range(0.6f, 0.8f);
            audioSource.volume = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(gunFire[1]);

            amountToReload = clipSize - currentClipSize;

            if (totalBullets >= amountToReload)
            {
                currentClipSize = clipSize;
                totalBullets -= amountToReload;
            }
            else
            {
                currentClipSize += totalBullets;
                totalBullets = 0;
            }
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        muzzleFlash.Play();

        // audioSource.clip = gunFire[0];
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.volume = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(gunFire[0]);

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            impactGO.transform.parent = hit.transform;
            Destroy(impactGO, impactDuration);
        }

        gunRecoil.Fire();

        currentClipSize--;
    }
}
