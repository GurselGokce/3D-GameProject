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
    public GameObject instructions;

    public void Start()
    {

        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length; //Array lengte van Enum
        currentEq = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        var x = instructions.GetComponentInChildren<UnityEngine.UI.Text>();
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
        Debug.Log("Called");

        currentEq[slotIndex] = newItem;
        x.text = $"{currentEq[slotIndex].name} Equipped,\n Damage: {currentEq[slotIndex].damage}, Speed: {1/currentEq[slotIndex].shootSpeed} Fire Rate";
        instructions.SetActive(true);
        StartCoroutine(InstructionsDelay(2));
    }

    public void Unequip(int slotIndex)
    {
        var x = instructions.GetComponentInChildren<UnityEngine.UI.Text>();
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

        x.text = $"All Unequipped,\n Damage: {0}, Speed: {1 / 0.5} Fire Rate";
        instructions.SetActive(true);
        StartCoroutine(InstructionsDelay(2));
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

    IEnumerator InstructionsDelay(float del)
    {
        yield return new WaitForSeconds(del);
        instructions.SetActive(false);
    }

}
