using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slidingdoor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject trigger;
    public GameObject door;
    public GameObject instructions;

    Animator anim;
    void Start()
    {
        anim = door.GetComponent<Animator>();
    }

    void OnTriggerStay(Collider collider)
    {   
        if (collider.gameObject.tag == "Player")
        {
            instructions.SetActive(true);
            //Animator anim = collider.GetComponentInChildren<Animator>();
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                anim.SetTrigger("OpenClose");
            }
                
        }
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            instructions.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
