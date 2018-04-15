using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySaveManager {

    public static List<Inventory> Inventories = new List<Inventory>();

    public static void SaveAllInventories()
    {
        foreach (var inventory in Inventories)
        {
            inventory.SaveInventory();
        }
    }

    public static void SaveInventoryByName(string inventoryName)
    {
        foreach (var inventory in Inventories)
        {
            if (inventory.InventoryName == inventoryName)
            {
                inventory.SaveInventory();
                break;
            }
        }
    }

    public static void LoadAllInventories()
    {
        foreach (var inventory in Inventories)
        {
            inventory.LoadInventory();
        }
    }

    public static void LoadInventoryByName(string inventoryName)
    {
        foreach (var inventory in Inventories)
        {
            if (inventory.InventoryName == inventoryName)
            {
                inventory.LoadInventory();
                break;
            }
        }
    }
}
