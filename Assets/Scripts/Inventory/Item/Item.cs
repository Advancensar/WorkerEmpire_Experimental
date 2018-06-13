using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{    
    public int ID;
    public string Name = "Empty";
    public Recipe Recipe = new Recipe();
    public Dictionary<string, ItemType> Type = new Dictionary<string, ItemType>();
    public int Price;

    public Item()
    {
        Type.Add("BaseType", new BaseType());
    }

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
        return SlotType.None;
    }

    public bool HasItemType(Type itemType)
    {
        foreach (var type in Type.Values)
        {
            if (itemType == type.GetType())
                return true;
        }
        return false;
    }

    public bool HasItemType(string itemType)
    {
        return Type.ContainsKey(itemType);
    }

    public ItemType GetTypeByName(string typeName)
    {
        return Type.ContainsKey(typeName) ? Type[typeName] : null;
    }

    public Stackable GetStackable()
    {
        return Type.ContainsKey("Stackable") ? (Stackable)Type["Stackable"] : null;
    }
}
