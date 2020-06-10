using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    //Singleton
    public static EquipmentManager instance;

    public void Awake()
    {
        instance = this;
    }

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);

    public OnEquipmentChanged onEquipmentChanged;

    public Equipment[] currentEq;
    Inventory inventory;

    public void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length; //Array lengte van Enum
        currentEq = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipmentSlot;

        Equipment oldItem = null;

        if (currentEq[slotIndex] != null)
        {
            oldItem = currentEq[slotIndex];
            inventory.Add(oldItem);
            //inventory.Add()
        }
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEq[slotIndex] = newItem;
    }

    public void Unequip(int slotIndex)
    {
        if (currentEq[slotIndex] != null)
        {
            Equipment oldItem = currentEq[slotIndex];
            inventory.Add(oldItem);
            currentEq[slotIndex] = null;
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

        }
    }

    public void UneqAll()
    {
        for (int i = 0; i < currentEq.Length; i++)
        {
            Unequip(i);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UneqAll();
        }
    }

}
