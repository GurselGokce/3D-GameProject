using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory instance; //Singleton Pattern

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of inventory");
        }
        instance = this;
    }


    public delegate void OnItemChange();
    public OnItemChange onItemChangeCall;

    public int limitedSpace = 5;

    public List<Item> items = new List<Item>();

    public bool Add(Item item)
    {
        if (!item.DefaultItem)
        {
            if (items.Count >= limitedSpace)
            {
                Debug.Log("Not enough room");
                return false;
            }
            items.Add(item);

            if (onItemChangeCall != null)
            {
                onItemChangeCall.Invoke();
            }
            
        }

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangeCall != null)
        {
            onItemChangeCall.Invoke();
        }
    }

}
