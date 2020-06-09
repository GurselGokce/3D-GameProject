using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject player;
    public static Inventory instance; //Singleton Pattern
    //public Item[] items = new Item[20];
    public GameObject[] removedItems = new GameObject[20];

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
        GameObject currentItem;

        //currentItem = Instantiate(item, player.transform.position,Quaternion.identity);
        //currentItem.position = 
        //removedItems[0] = item;

        //currentItem = Instantiate(removedItems[0], player.transform.position, Quaternion.identity);
        //currentItem.transform.position = player.transform.position;

        items.Remove(item);

        //for (int i = 0; i < 1; i++)
        //{
        //    if (items[i] == item)
        //    {


        //        if(items[i] == null)
        //        {

        //        }
        //    }
        //}


        if (onItemChangeCall != null)
        {
            onItemChangeCall.Invoke();
        }
    }

}
