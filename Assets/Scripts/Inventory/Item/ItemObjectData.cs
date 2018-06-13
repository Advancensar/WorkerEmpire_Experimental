using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemObjectData
{
    public string InventoryName;
    public int SlotNumber;
    public Item item = new Item();


    public void Consume(int amount = 0)
    {
        if (amount > 0)
        {
            item.GetStackable().CurrentStack -= amount;
            if (item.GetStackable().CurrentStack <= 0)
            {
                InventoryManager.Instance.GetInventoryByName(InventoryName).Items.Remove(SlotNumber);
            }
        }
        else
        {
            InventoryManager.Instance.GetInventoryByName(InventoryName).Items.Remove(SlotNumber);

        }
    }

}
