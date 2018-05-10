using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryWindow : MonoBehaviour {

    public Dictionary<int, GameObject> Slots = new Dictionary<int, GameObject>();

    private void Awake()
    {
        InstantiateSlots();
        //LoadWindowInfo();
    }

    private void Start()
    {
        //LoadWindowInfo();
    }

    public void LoadWindowInfo(Inventory Inventory)
    {
        var ItemObjectPrefab = Resources.Load<GameObject>("Prefabs/UI/ItemObject");
        //cityStorage.LoadInventory();

        //InstantiateItems(cityStorage.Inventory);
        foreach (var key in Inventory.Items.Keys)
        {
            var itemObjectData = Inventory.Items[key];
            var item = Instantiate(ItemObjectPrefab);
            item.GetComponent<ItemObject>().ItemObjectData = itemObjectData;
            item.transform.SetParent(Slots[itemObjectData.SlotNumber].transform, worldPositionStays: false);
            item.GetComponent<ItemObject>().LoadItemInfo();
        }
    }

    public void InstantiateSlots()
    {
        var slotContent = transform.Find("Viewport").Find("Content");
        for (int i = 0; i < slotContent.childCount; i++)
        {
            var slot = slotContent.GetChild(i).gameObject;
            slot.GetComponent<Slot>().SlotNumber = slot.transform.GetSiblingIndex();
            Slots.Add(slot.GetComponent<Slot>().SlotNumber, slot);
            //slot.SetActive(false);

            //Slots[i].GetComponent<Slot>().SlotNumber = i;
        }
    }
}
