using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager
{
    public static InventoryManager Instance { get; } = new InventoryManager();

    public Dictionary<string, Inventory> Inventories = new Dictionary<string, Inventory>();

    private string PATH = @"/Database/Inventories/";

    public void AddInventoryToInventoryManager(Inventory inventory)
    {
        Instance.Inventories.Add(inventory.InventoryName, inventory);
    }

    public void SaveInventory(Inventory inventory)
    {
        //PATH += InventoryName + ".json";
        var templist = new List<ItemObjectData>();
        foreach (var slotNumber in inventory.Items.Keys)
        {
            templist.Add(inventory.Items[slotNumber]);
        }
        //Debug.Log(InventoryName + " tc : " + templist.Count + "" + Items.Count + "" + InventoryManager.GetInventoryByName("Player"));

        FileTool.SaveFileAsJson(PATH + inventory.InventoryName + ".json", templist);

        //LoadInventory();
    }

    public void LoadInventory(Inventory inventory)
    {
        //PATH += InventoryName + ".json";
        inventory.Items.Clear();
        Debug.Log("LoadInventory: " + PATH);
        var tempItems = FileTool.LoadObjectFromJson<List<ItemObjectData>>(PATH + inventory.InventoryName + ".json");

        if (tempItems != null)
        {
            if (tempItems.Count < 1)
                return;

            for (int i = 0; i < tempItems.Count; i++)
            {
                inventory.Items.Add(tempItems[i].SlotNumber, tempItems[i]);
            }
        }
    }

    public Inventory GetInventoryByName(string inventoryName)
    {
        if (Instance.Inventories.ContainsKey(inventoryName))
        {
            return Instance.Inventories[inventoryName];
        }
        return null;
    }

    public void SaveAllInventories()
    {
        foreach (var key in Instance.Inventories.Keys.ToList())
        {
            SaveInventoryByName(key);
        }
    }

    public void SaveInventoryByName(string inventoryName)
    {
        Instance.SaveInventory(Instance.Inventories[inventoryName]);
    }

    public void LoadAllInventories()
    {
        foreach (var key in Inventories.Keys.ToList())
        {
            LoadInventoryByName(key);
        }
    }

    public void LoadInventoryByName(string inventoryName)
    {
        Instance.LoadInventory(Instance.Inventories[inventoryName]);

    }
}
