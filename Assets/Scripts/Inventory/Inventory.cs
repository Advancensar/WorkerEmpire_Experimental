using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public string InventoryName;
    public int InventorySize = 23;
    private string PATH = @"/Database/";
    

    //public void SaveInventory(string name)
    //{
    //    var templist = new List<ItemObjectData>();
    //    foreach (var key in SaveManager.Inventory[name].Keys)
    //    {
    //        templist.Add(SaveManager.Inventory[name][key].GetComponent<Slot>().HeldItem.GetComponent<ItemObject>().itemObjectData);
    //    }
    //    FileTool.SaveFileAsJson(name, templist);
    //}

    public void CreateInventory(string inventoryName)
    {
        InventoryName = inventoryName;
        if (!SaveManager.Inventory.ContainsKey(inventoryName))
        {
            SaveManager.Inventory.Add(inventoryName, new Dictionary<int, ItemObjectData>());
        }
    }
    
    public void SaveInventory()
    {
        PATH += InventoryName + ".json";
        var templist = new List<ItemObjectData>();

        foreach (var slotNumber in SaveManager.Inventory[InventoryName].Keys)
        {
            templist.Add(SaveManager.Inventory[InventoryName][slotNumber]);
        }

        FileTool.SaveFileAsJson(PATH, templist);
    }

    public void LoadInventory()
    {
        PATH += InventoryName + ".json";
        Debug.Log("LoadInventory: " + PATH);
        var tempItems = FileTool.LoadObjectFromJson<List<ItemObjectData>>(PATH);

        if (tempItems.Count < 1)
            return;

        for (int i = 0; i < tempItems.Count; i++)
        {
            SaveManager.Inventory[InventoryName].Add(tempItems[i].SlotNumber, tempItems[i]);
        }

        AddToList();
    }

    public void AddToList()
    {
        InventorySaveManager.Inventories.Add(this);
    }

    public void ClearInventory()
    {
        foreach (var key in Slot.Slots.Keys)
        {
            GameObject.Destroy(Slot.Slots[key].GetComponent<Slot>().HeldItem);
        }
        //Instance.Items.Clear();
    }


    int FindFirstAvailableSlot()
    {
        for (int i = Slot.inventoryOffset; i < Slot.Slots.Count; i++)
        {
            if (Slot.Slots[i].GetComponent<Slot>().HeldItem == null)
            {
                //Debug.Log("Slot found : " + i);
                return i;
            }
        }
        return -1;
    }

    void AddItemToSlot(int id, GameObject item)
    {
        if (Slot.Slots[id].GetComponent<Slot>().HeldItem == null)
        {
            item.transform.SetParent(Slot.Slots[id].transform, false);
            item.transform.localScale = Vector3.one;
        }
        //if (Slots[id].GetComponent<SlotHandler>().HoldItem == null)
        //{
        //    var tempItem = Instantiate(itemPrefab);
        //    tempItem.GetComponent<ItemObject>().item = item;
        //    tempItem.name = item.itemName;
        //    tempItem.transform.SetParent(SlotList[id].transform);
        //    tempItem.transform.localScale = Vector3.one;
        //}
        else
        {
            GameObject.Destroy(item);
            Debug.Log("Slot is full" + " " + Slot.Slots[id].GetComponent<Slot>().SlotNumber);
        }
    }




    public void AddRandomItemToRandomSlot()
    {
        int id = FindFirstAvailableSlot();
        //int itemid = Random.Range(0, ItemDatabase.Instance.Items.Count-1);
        if (id >= 0)
        {
            var randomItem = ItemDatabase.Instance.RandomItem();
            //var item = ItemDatabase.GetItemCopy(UnityEngine.Random.Range(0, ItemDatabase.GetDBLength()));
            var itemObjectGameObject = GameObject.Instantiate(Resources.Load(@"Prefabs/UI/ItemObject")) as GameObject;
            itemObjectGameObject.GetComponent<ItemObject>().itemObjectData.item = randomItem;
            //item.Name = "item_1";
            //item.ID = 5;
            itemObjectGameObject.GetComponent<ItemObject>().LoadItemInfo();
            AddItemToSlot(id, itemObjectGameObject);
            //Items.Add(itemObjectGameObject.GetComponent<ItemObject>());
        }
        else Debug.Log("Inventory is full");
    }

}
