using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/item")]
public class Item : ScriptableObject
{
    new public string name = "Item";
    public Sprite icon = null;
    public bool DefaultItem = false;


}
