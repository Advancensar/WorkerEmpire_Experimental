using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public string InventoryName;
    public int InventorySize = 24;
    public Dictionary<int, ItemObjectData> Items = new Dictionary<int, ItemObjectData>();

    public void ReplaceItem(int slotNumber, ItemObjectData itemObjectData)
    {
        if (Items.ContainsKey(slotNumber))
        {
            //Debug.Log("Removed : " + slotNumber + "iod : " + Items[slotNumber].item.Name);
            Items.Remove(slotNumber);
        }
        Items.Add(slotNumber, itemObjectData);
    }

    public void AddItemToSlot(int slotNumber, ItemObjectData iod)
    {
        if (iod.item.HasItemType("Stackable")) // stackable varsa add to stack
        {
            var matchingSn = GetItemSlotNumber(iod.item);
            if (matchingSn > -1) // inventoryde varsa
            {
                Items[matchingSn].item.GetStackable().AddToStack(iod.item.GetStackable().CurrentStack);
                Debug.Log("additem");
            }
            else
            {
                ReplaceItem(slotNumber, iod);
            }
        }
        else
        {
            ReplaceItem(slotNumber, iod);
            Debug.Log("Added item to slot" + slotNumber + " : " + iod.item.Name);
        }

        if (GameManager.Instance.InventoryWindow.CurrentInventory == null) return;


        if (GameManager.Instance.InventoryWindow.CurrentInventory.InventoryName == InventoryName)
        {
            Debug.Log("Refreshed slot : " + slotNumber);
            GameManager.Instance.InventoryWindow.RefreshSlot(slotNumber);
        }



    }

    public void AddItem(Item item)
    {
        var iod = new ItemObjectData()
        {
            InventoryName = InventoryName,
            item = item,
            SlotNumber = FirstAvailableSlot(item)
        };
        Debug.Log("Craft slot : " + iod.SlotNumber);
        AddItemToSlot(iod.SlotNumber, iod);
    }

    public int GetItemSlotNumber(Item item)
    {
        foreach (var slotNumber in Items.Keys.ToList())
        {
            if (Items[slotNumber].item.ID == item.ID)
            {
                return slotNumber;
            }
        }
        return -1;
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

    public int ItemCount(int itemID)
    {
        //for (int i = 0; i < UPPER; i++)
        //{
            
        //}
        return 0;
    }

    public int ItemCount(string itemName)
    {
        return 0;
    }

    public int FirstAvailableSlot(Item item)
    {
        if (item.HasItemType("Stackable"))
        {
            foreach (var key in Items.Keys.ToList())
            {
                if (Items[key].item.ID == item.ID)
                {
                    return key;
                }
            }
        }
        else
        {
            return FirstAvailableSlot();
        }

        return -1;
    }

    public int FirstAvailableSlot()
    {
        for (int i = 0; i < InventorySize; i++)
        {
            if (!Items.ContainsKey(i))
            {
                return i;
            }

        }

        return -1;
    }

    public bool HasItem(ItemObjectData iod)
    {
        foreach (var key in Items.Keys.ToList())
        {
            if (Items[key].item.ID == iod.item.ID)
            {
                return true;
            }
        }

        return false;
    }


}
