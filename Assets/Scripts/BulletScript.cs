using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject hitEffect;

    public GameObject gameManager;

    public float TimeToLive = 5f;
    int enemyCount;

    //public LayerMask enemyLayers;

    public float mSpeed = 10.0f;

    Vector3 mPrevPos;

    private void Start()
    {
        mPrevPos = transform.position;

        Destroy(gameObject, TimeToLive); //Bullet gaat weg na time to live
        //enemyCount = gameManager.GetComponent<SpawnEnemies>().enemyCount;
        Debug.Log(gameManager.GetComponent<SpawnEnemies>().enemyCount);
    }

    void Update()
    {

        mPrevPos = transform.position;

        transform.Translate(0.0f, 0.0f, mSpeed * Time.deltaTime);

        RaycastHit[] hits = Physics.RaycastAll(new Ray(mPrevPos, (transform.position - mPrevPos).normalized), (transform.position - mPrevPos).magnitude);

        foreach (var hit in hits)
        {
            //Debug.Log(hit.collider.gameObject.name);
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f); //Effect gaat weg na 5 seconde

            //Verander name naar tag
            if(hit.collider.gameObject.tag != "Door") //Bullets only destroy when hitting objects other than doors, making it possible to hit enemies through doors.
            {
                Destroy(gameObject);
            }
            if (hit.collider.gameObject.tag == "Enemy")
            {

                Destroy(hit.collider.gameObject);
                gameManager.GetComponent<SpawnEnemies>().enemyCount -= 1;
                Debug.Log(gameManager.GetComponent<SpawnEnemies>().enemyCount);
            }

            //Destroy(gameObject); //Bullets gaan weg na collisie

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
