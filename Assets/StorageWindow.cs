using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageWindow : MonoBehaviour {

    public GameObject SlotPrefab;
    public GameObject ItemObjectPrefab;

    Transform slotContent;   

    public void LoadWindowInfo(CityStorage cityStorage)
    {
        slotContent = transform.Find("Viewport").Find("Content");
        ClearContent();
        InstantiateSlots(cityStorage.Inventory);
        //cityStorage.LoadInventory();
        InstantiateItems(cityStorage.gameObject.name);
    }

    public void InstantiateItems(string cityName)
    {
        foreach (var key in SaveManager.Inventory[cityName].Keys)
        {
            var itemObjectData = SaveManager.Inventory[cityName][key];
            var item = Instantiate(ItemObjectPrefab, slotContent.GetChild(itemObjectData.SlotNumber), worldPositionStays: false);
            item.GetComponent<ItemObject>().itemObjectData = itemObjectData;
        }
            //Slots[i].GetComponent<Slot>().SlotNumber = i;
    }

    public void InstantiateSlots(Inventory Storage)
    {
        for (int i = 0; i < Storage.InventorySize; i++)
        {
            var slot = Instantiate(SlotPrefab, slotContent, worldPositionStays: false);            
            //Slots[i].GetComponent<Slot>().SlotNumber = i;
        }
    }

    void ClearContent()
    {
        foreach (Transform child in slotContent)
        {
            Destroy(child.gameObject);
        }
    }


}
