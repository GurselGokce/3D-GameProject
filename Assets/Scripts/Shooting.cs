using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    public GameObject gameManager;
    public EquipmentManager EquipmentList;
    private void Start()
    {
        gameManager = GameObject.Find("GameManager");
        EquipmentList = gameManager.GetComponent<EquipmentManager>();

    }


    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) //Je kan niet schieten als jouw muis over inventory is
        {
            return;
        }
        //Debug.Log(EquipmentList.currentEq[0]);
        if (EquipmentList.currentEq[0] != null)
        {
            shootDelay = EquipmentList.currentEq[0].shootSpeed;
        }
        else
        {
            shootDelay = 0.5f; //Default shoot snelheid
        }


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
        shoot = false;
        StartCoroutine(ShootDelay());


    }
}
