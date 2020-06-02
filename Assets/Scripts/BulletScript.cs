using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject hitEffect;

    public float TimeToLive = 5f;

    private void Start()
    {
        Destroy(gameObject, TimeToLive); //Bullet gaat weg na time to live
    }

    void OnCollisionEnter(Collision collision)
    {
        //collision.collider //Collider that we made contact with (enemy of dergelijke)
        //collision.relativeVelocity //velocity between the objects hit
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f); //Effect gaat weg na 5 seconde
        Destroy(gameObject); //Bullets gaan weg na collisie
    }

}
