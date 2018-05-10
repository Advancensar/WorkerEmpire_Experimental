using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public string InventoryName;
    public int InventorySize = 24;
    public Dictionary<int, ItemObjectData> Items = new Dictionary<int, ItemObjectData>();

    public void AddItemToSlot(int slotNumber, ItemObjectData itemObjectData)
    {
        if (Items.ContainsKey(slotNumber))
        {
            //Debug.Log("Removed : " + slotNumber + "iod : " + Items[slotNumber].item.Name);
            Items.Remove(slotNumber);
        }
        Items.Add(slotNumber, itemObjectData);
    }

    public void RemoveItem(int slotNumber)
    {
        //Debug.Log("Slot number : " + slotNumber);

        if (Items.ContainsKey(slotNumber))
        {
            Items.Remove(slotNumber);
        }
    }

    public void MoveItem(int fromSlot, int toSlot)
    {
        if (Items.ContainsKey(toSlot))//Swap
        {
            var tempItem = Items[fromSlot];
            Items[fromSlot] = Items[toSlot];
            Items[toSlot] = tempItem;
        }
        else
        {
            Items[toSlot] = Items[fromSlot];
            Items.Remove(fromSlot);
        }
    }



}
