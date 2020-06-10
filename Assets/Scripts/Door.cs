using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
    public GameObject instructions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player"  )
        {
            instructions.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                //if (true)//key controll
                
                //Debug.Log("Collision door detected");
                //SceneManager.LoadScene("Level2");
            }

        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            instructions.SetActive(false);
            //Debug.Log("Collision door detected");
            //SceneManager.LoadScene("Level2");
        }
    }
}
