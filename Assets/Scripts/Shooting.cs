using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float shootDelay = 0.5f;
    float M_LastPressTime;
    bool shoot = true;
    public AudioSource shootSFX;

    //public float bulletForce = 20f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && shoot)
        {
            Shoot();
            shootSFX.Play();
        }

    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootDelay);
        shoot = true;

    }

    void Shoot()
    {

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        //rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        shoot = false;
        StartCoroutine(ShootDelay());


    }
}
