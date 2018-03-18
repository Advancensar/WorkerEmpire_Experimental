using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour {

    public static List<ItemObject> Items = new List<ItemObject>();

    public int InventorySize = 16;
    public GameObject SlotPrefab;
    public GameObject ItemObjectPrefab;

    List<GameObject> Slots = new List<GameObject>();
    
    void Awake () {
        var slots = transform.Find("Viewport").transform.Find("Content");
        for (int i = 0; i < InventorySize; i++)
        {
            Slots.Add(Instantiate(SlotPrefab, slots, worldPositionStays: false));
            Slots[i].GetComponent<StorageSlot>().SlotNumber = i;
        }
	}
    
    public void SaveInventory()
    {        
        var templist = new List<ItemObjectData>();
        foreach (var item in Items)
        {
            templist.Add(item.itemObjectData);
        }
        FileTool.SaveFileAsJson(Application.dataPath + @"/Resources/Database/Storage.json", templist);        
    }
    public void LoadInventory()
    {
        Debug.Log("LoadInventory");
        var tempItems = FileTool.LoadObjectFromJson<List<ItemObjectData>>(Application.dataPath + @"/Resources/Database/Storage.json");

        for (int i = 0; i < tempItems.Count; i++)
        {
            var item = Instantiate(ItemObjectPrefab);
            var itemObject = item.GetComponent<ItemObject>();
            itemObject.itemObjectData.item = tempItems[i].item;
            itemObject.itemObjectData.SlotNumber = tempItems[i].SlotNumber;
            itemObject.LoadItemInfo();


            //var s = Slots[i];
            //item.transform.SetParent(s.transform);
            //Debug.Log("Slot" + Slots[i].GetComponent<SlotHandler>().SlotNumber);
            //item.transform.SetParent(Slots[0].transform);

            item.transform.SetParent(Slots[tempItems[i].SlotNumber].transform);
            //Items.Add(itemObject);

            //Slots[Items[1].SlotNumber].GetComponent<SlotHandler>().HeldItem
        }
    }


    public void AddRandomItemToRandomSlot()
    {
        int id = FindFirstAvailableSlot();
        int itemid = Random.Range(0, ItemDatabase.Instance.Items.Count);
        if (id >= 0)
        {
            var randomItem =ItemDatabase.Instance.GetItem(itemid);
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
        for (int i = 0; i < Slots.Count; i++)
        {

            if (Slots[i].GetComponent<StorageSlot>().HeldItem == null)
            {
                //Debug.Log("Slot found : " + i);
                return i;
            }
        }
        return -1;
    }
    void AddItemToSlot(int id, GameObject item)
    {
        if (Slots[id].GetComponent<StorageSlot>().HeldItem == null)
        {
            item.transform.SetParent(Slots[id].transform);
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
            Debug.Log("Inventory is full");
        }
    }
}
