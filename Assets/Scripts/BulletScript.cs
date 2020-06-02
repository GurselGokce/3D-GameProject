using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject hitEffect;

    public float TimeToLive = 5f;

    public LayerMask enemyLayers;

    public float mSpeed = 10.0f;

    Vector3 mPrevPos;

    private void Start()
    {
        mPrevPos = transform.position;

        Destroy(gameObject, TimeToLive); //Bullet gaat weg na time to live
    }

    void Update()
    {

        mPrevPos = transform.position;

        transform.Translate(0.0f, 0.0f, mSpeed * Time.deltaTime);

        RaycastHit[] hits = Physics.RaycastAll(new Ray(mPrevPos, (transform.position - mPrevPos).normalized), (transform.position - mPrevPos).magnitude);

        foreach (var hit in hits)
        {
            Debug.Log(hit.collider.gameObject.name);
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f); //Effect gaat weg na 5 seconde
            Destroy(gameObject); //Bullets gaan weg na collisie

        }


    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.collider.name);

        //if (collision.collider.name == "Enemy")
        //{
        //    Debug.Log("Enemy Hit");
        //}

        //collision.relativeVelocity //velocity between the objects hit
        //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 0.5f); //Effect gaat weg na 5 seconde
        //Destroy(gameObject); //Bullets gaan weg na collisie

        //RaycastHit[] hits = Physics.OverlapSphere

        //collision.collider; //Collider that we made contact with (enemy of dergelijke)
    }



}
