using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/item")]
public class Item : ScriptableObject
{
    new public string name = "Item";
    public Sprite icon = null;
    public bool DefaultItem = false;

    public GameObject pfItemWorld;

    public void RemoveFromInventory()
    {
        Inventory.instance.Equip(this);
    }

    public virtual void Use()
    {
        //use item

        Debug.Log("Using " + name);
    }

    


}
