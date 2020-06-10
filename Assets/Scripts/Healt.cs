using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healt : MonoBehaviour
{
    // Start is called before the first frame update
    public int healthPlayer =30;
    public int damage =10;
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "dsada")
        {
            healthPlayer -= damage;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
