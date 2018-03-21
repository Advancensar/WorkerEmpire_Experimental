using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{    
    public int ID;
    public string Name = "Empty";
    public Dictionary<string, ItemType> Type = new Dictionary<string, ItemType>();

    public Sprite Image()
    {
        return Resources.Load<Sprite>("Sprites/Items/" + Name);
    }

    public SlotType slotType()
    {
        if (Type.ContainsKey("Equippable"))
        {
            return ((Equippable)Type["Equippable"]).SlotType;
        }
        return SlotType.All;
    }

}
