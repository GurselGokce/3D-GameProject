using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Interact
{
    public Item item;
    public override void DoInteract()
    {
        base.DoInteract();

        Pick();
    }

    void Pick()
    {
        
        Debug.Log("Pick up item " + item.name);
        bool pickedUp = Inventory.instance.Add(item);

        if (pickedUp)
        {
            Destroy(gameObject);
        }
    }

}
