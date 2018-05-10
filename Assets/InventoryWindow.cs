using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWindow : MonoBehaviour {

    public GameObject SlotPrefab;
    public GameObject ItemObjectPrefab;

    public int MaxSlot = 30;
    public Dictionary<int, GameObject> Slots = new Dictionary<int, GameObject>();

    private bool DynamicInventory = true;
    private Transform slotContent;

    private void Awake()
    {
        slotContent = transform.Find("Viewport").Find("Content");

        if (gameObject.name == "PlayerInventoryWindow")
        {
            DynamicInventory = false;
            MaxSlot = slotContent.childCount;
            var inv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().Inventory;
            InstantiateSlots();
            LoadWindowInfo(inv);
        }
        else
        {
            InstantiateSlots();
            LoadWindowInfo(null);
            
        }
        gameObject.SetActive(false);
    }

    public void LoadWindowInfo(Inventory Inventory)
    {
        gameObject.SetActive(true);
        if (Inventory == null)
        {
            return;
        }
        InstantiateItems(Inventory);
    }

    public void InstantiateItems(Inventory Inventory)
    {
        if (DynamicInventory)
        {
            foreach (var key in Slots.Keys)
            {
                Slots[key].GetComponent<Slot>().InventoryName = Inventory.InventoryName;
            }

            for (int i = 0; i < MaxSlot; i++) // Clear content and enable slots equal to inventory size
            {
                Destroy(Slots[i].GetComponent<Slot>().HeldItem);
                Slots[i].GetComponent<Slot>().HeldItem = null;

                if (Inventory.InventorySize > i)
                {
                    Slots[i].SetActive(true);
                }
                else
                {
                    Slots[i].SetActive(false);
                }
            }
        }


        //Debug.Log("Inventory name : " + Inventory.InventoryName);

        foreach (var key in Inventory.Items.Keys)
        {
            var itemObjectData = Inventory.Items[key];
            //Debug.Log("Key: " + key + "iod" + itemObjectData.SlotNumber);

            var item = Instantiate(ItemObjectPrefab);
            item.GetComponent<ItemObject>().ItemObjectData = itemObjectData;
            item.transform.SetParent(Slots[itemObjectData.SlotNumber].transform, worldPositionStays: false);
            item.GetComponent<ItemObject>().LoadItemInfo();
        }
        //Slots[i].GetComponent<Slot>().SlotNumber = i;
    }

    public void InstantiateSlots()
    {
        for (int i = 0; i < MaxSlot; i++)
        {
            GameObject slot;
            if (DynamicInventory)
            {
                slot = Instantiate(SlotPrefab, slotContent, worldPositionStays: false);
                slot.SetActive(false);
            }
            else
            {
                slot = slotContent.GetChild(i).gameObject;
            }

            slot.GetComponent<Slot>().SlotNumber = slot.transform.GetSiblingIndex();
            Slots.Add(slot.GetComponent<Slot>().SlotNumber, slot);

            //Slots[i].GetComponent<Slot>().SlotNumber = i;
        }
    }    

    public void AddButton()
    {
        var iod = new ItemObjectData
        {
            item = ItemDatabase.Instance.RandomItem(),
            SlotNumber = -77
        };
        AddItemToSlot(FindFirstAvailableSlot(), iod);
        //AddRandomItemToFirstAvailableSlot();        
    }

    private void AddItemToSlot(int slotNumber, ItemObjectData iod)
    {
        var itemObject = Instantiate(ItemObjectPrefab);
        itemObject.GetComponent<ItemObject>().ItemObjectData = iod;
        itemObject.GetComponent<ItemObject>().LoadItemInfo();
        itemObject.transform.SetParent(Slots[slotNumber].transform);
    }

    int FindFirstAvailableSlot()
    {
        for (int i = 0; i < MaxSlot; i++)
        {
            if (Slots[i].GetComponent<Slot>().HeldItem == null && Slots[i].activeSelf)
            {
                //Debug.Log("Slot found : " + i);
                return i;
            }
        }
        return -1;
    }

    //void AddItemToSlot(int id, GameObject item)
    //{
    //    Debug.Log("Adding item to slot : " + id);
    //    if (Slots[id].GetComponent<Slot>().HeldItem == null)
    //    {
    //        item.transform.SetParent(Slots[id].transform, false);
    //        var iod = item.GetComponent<ItemObject>().itemObjectData;
    //        Debug.Log(iod.InventoryName);
    //        InventoryManager.GetInventoryByName(iod.InventoryName).Items.Add(id, iod);
    //        item.transform.localScale = Vector3.one;
    //    }
    //    //if (Slots[id].GetComponent<SlotHandler>().HoldItem == null)
    //    //{
    //    //    var tempItem = Instantiate(itemPrefab);
    //    //    tempItem.GetComponent<ItemObject>().item = item;
    //    //    tempItem.name = item.itemName;
    //    //    tempItem.transform.SetParent(SlotList[id].transform);
    //    //    tempItem.transform.localScale = Vector3.one;
    //    //}
    //    else
    //    {
    //        GameObject.Destroy(item);
    //        Debug.Log("Slot is full" + " " + Slots[id].GetComponent<Slot>().SlotNumber);
    //    }
    //}

    //public void AddRandomItemToFirstAvailableSlot()
    //{
    //    int id = FindFirstAvailableSlot();
    //    //int itemid = Random.Range(0, ItemDatabase.Instance.Items.Count-1);
    //    if (id >= 0)
    //    {
    //        var randomItem = ItemDatabase.Instance.RandomItem();
    //        //var item = ItemDatabase.GetItemCopy(UnityEngine.Random.Range(0, ItemDatabase.GetDBLength()));
    //        var itemObjectGameObject = GameObject.Instantiate(Resources.Load(@"Prefabs/UI/ItemObject")) as GameObject;
    //        itemObjectGameObject.GetComponent<ItemObject>().itemObjectData.item = randomItem;
    //        itemObjectGameObject.GetComponent<ItemObject>().itemObjectData.InventoryName = Slots[0].GetComponent<Slot>().InventoryName;
    //        //item.Name = "item_1";
    //        //item.ID = 5;
    //        itemObjectGameObject.GetComponent<ItemObject>().LoadItemInfo();
    //        AddItemToSlot(id, itemObjectGameObject);
    //        //Items.Add(itemObjectGameObject.GetComponent<ItemObject>());
    //    }
    //    else Debug.Log("Inventory is full");
    //}

}
