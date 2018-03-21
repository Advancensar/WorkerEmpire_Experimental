using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour {

    public static List<ItemObject> Items = new List<ItemObject>();

    public int InventorySize = 16;
    public GameObject SlotPrefab;
    public GameObject ItemObjectPrefab;

    
    void Awake () {
        var slots = transform.Find("Viewport").transform.Find("Content");
        for (int i = 0; i < InventorySize; i++)
        {
            Instantiate(SlotPrefab, slots, worldPositionStays: false);
            //Slots[i].GetComponent<Slot>().SlotNumber = i;
        }
	}
        
    public void SaveInventory()
    {        
        var templist = new List<ItemObjectData>();
        foreach (var item in Items)
        {
            templist.Add(item.itemObjectData);
        }
        FileTool.SaveFileAsJson(@"/Database/Storage.json", templist);        
    }

    public void LoadInventory()
    {
        Debug.Log("LoadInventory");
        var tempItems = FileTool.LoadObjectFromJson<List<ItemObjectData>>(@"/Database/Storage.json");

        for (int i = 0; i < tempItems.Count; i++)
        {
            var item = Instantiate(ItemObjectPrefab);

            var itemObject = item.GetComponent<ItemObject>();
            itemObject.itemObjectData.item = tempItems[i].item;
            itemObject.itemObjectData.SlotNumber = tempItems[i].SlotNumber;
            itemObject.LoadItemInfo();
            AddItemToSlot(itemObject.itemObjectData.SlotNumber, item);


            //var s = Slots[i];
            //item.transform.SetParent(s.transform);
            //Debug.Log("Slot" + Slots[i].GetComponent<SlotHandler>().SlotNumber);
            //item.transform.SetParent(Slots[0].transform);
            //Debug.Log("Slot number : " + tempItems[i].SlotNumber);
            //Debug.Log("Gameobject : " + Slot.Slots[tempItems[i].SlotNumber].gameObject.name);
            //Debug.Log(Slot.Slots[1])
            //item.transform.SetParent(Slot.Slots[tempItems[i].SlotNumber].transform);
            //Items.Add(itemObject);

            //Slots[Items[1].SlotNumber].GetComponent<SlotHandler>().HeldItem
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
            var itemObjectGameObject = Instantiate(ItemObjectPrefab);
            itemObjectGameObject.GetComponent<ItemObject>().itemObjectData.item = randomItem;
            //item.Name = "item_1";
            //item.ID = 5;
            itemObjectGameObject.GetComponent<ItemObject>().LoadItemInfo();
            AddItemToSlot(id, itemObjectGameObject);
            //Items.Add(itemObjectGameObject.GetComponent<ItemObject>());
        }
        else Debug.Log("Inventory is full");
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
            Debug.Log("Slot is full" + " " + Slot.Slots[id].GetComponent<Slot>().SlotNumber);
        }
    }
}
