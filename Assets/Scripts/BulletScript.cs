using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject hitEffect;

    void OnCollisionEnter(Collision collision)
    {
        //collision.collider //Collider that we made contact with (enemy of dergelijke)
        //collision.relativeVelocity //velocity between the objects hit
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1f); //Bullets gaan weg na 5 seconde
        Destroy(gameObject); //Bullets gaan weg na collisie
    }

}
