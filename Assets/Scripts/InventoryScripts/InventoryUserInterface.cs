using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUserInterface : MonoBehaviour
{
    public GameObject InventoryUI;
    public Transform itemsParent;


    Inventory inventory;

    InventorySlot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangeCall += UpdateInterface;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            InventoryUI.SetActive(!InventoryUI.activeSelf);
        }
    }

    void UpdateInterface()
    {
        for(int i = 0; i<slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
