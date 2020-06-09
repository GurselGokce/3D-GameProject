using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slidingdoor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject trigger;
    public GameObject door;

    Animator anim;
    void Start()
    {
        anim = door.GetComponent<Animator>();
    }

    void OnTriggerStay(Collider collider)
    {
        Debug.Log("deur");
        if (collider.gameObject.tag == "Player")
        {
            //Animator anim = collider.GetComponentInChildren<Animator>();
            Debug.Log("ik ben bij de deur");
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("OpenClose");
                anim.SetTrigger("OpenClose");
            }
                
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
