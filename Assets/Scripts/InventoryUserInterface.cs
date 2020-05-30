using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInterface : MonoBehaviour
{
    Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangeCall += UpdateInterface;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateInterface()
    {
        Debug.Log("Updating UI");
    }
}
