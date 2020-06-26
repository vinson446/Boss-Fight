using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPlayerWeapon : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        currentClipSize = clipSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1) && Time.time >= nextTimeToFire && currentClipSize > 0)
        {
            // greater the fireRate, faster it is
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            float amountToReload;

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

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

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
            Destroy(impactGO, impactDuration);
        }

        currentClipSize--;
    }
}
