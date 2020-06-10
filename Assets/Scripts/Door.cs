using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Door : MonoBehaviour
{
    public GameObject instructions;
    // Start is called before the first frame update
    GameObject gameManager;
    Inventory itemsList;
    //Inventory inv = new Inventory();
    bool deurActivated = false;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        itemsList = gameManager.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && (deurActivated))
        {
            print("Level2");
            //SceneManager.LoadScene("Level2");
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        var x = instructions.GetComponentInChildren<UnityEngine.UI.Text>();
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player Detected");
            

            if (itemsList.items.Count != 0)
            {
                var zoekKey = itemsList.items.Where(key => key.name.Contains("KeyL1")).FirstOrDefault();
                Debug.Log(zoekKey);

                if(zoekKey != null)
                {
                    Debug.Log("Level 2 activated");
                    x.text = "Druk 'E' voor de volgende level";
                    instructions.SetActive(true);
                    deurActivated = true;
                    //if (Input.GetKeyDown(KeyCode.E))
                    //{
                    //    print("Level2");
                        
                    //    //SceneManager.LoadScene("Level2");
                    //}
                }
                else
                {
                    deurActivated = false;
                    x.text = "Vind de keycard om naar de volgende level te geraken";
                    instructions.SetActive(true);
                }

            }
            else
            {
                deurActivated = false;
                x.text = "Vind de keycard om naar de volgende level te geraken";
                instructions.SetActive(true);

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
