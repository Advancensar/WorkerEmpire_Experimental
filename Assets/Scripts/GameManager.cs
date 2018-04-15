using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    
    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Start()
    {
        //Storage.Instance.LoadInventory();
        //LoadPlayerHouseData();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        LoadItemDB();
        //Storage.Instance.InstantiateSlots();

    }

    void Load()
    {
        //Load Item DB
        //Load Player Inventory
        //Load Player House Data
    }

    void LoadItemDB()
    {
        ItemDatabase.Instance.LoadDB();
    }

    void LoadStorage()
    {
        Debug.Log("Instantiated Slots");
        //Storage.Instance.LoadInventory();
        Debug.Log("Loaded inventory");

        //var slots = transform.Find("Viewport").transform.Find("Content");
        //for (int i = 0; i < InventorySize; i++)
        //{
        //    Instantiate(SlotPrefab, slots, worldPositionStays: false);
        //    //Slots[i].GetComponent<Slot>().SlotNumber = i;
        //}
    }

    public void LoadPlayerHouseData()
    {
        HouseSaveManager.LoadPlayerHouseData();
    }



    public void btn()
    {
        //Storage.Instance.AddRandomItemToRandomSlot();
    }

    public void save()
    {
        //Storage.Instance.SaveInventory();
    }
    public void load()
    {
        //Storage.Instance.LoadInventory();
    }
}
