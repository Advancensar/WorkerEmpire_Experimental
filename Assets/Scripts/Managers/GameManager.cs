using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HouseWindow HouseWindow;
    public InventoryWindow InventoryWindow;
    public CraftWindow CraftWindow;
    public WorkerWindow WorkerWindow;

    public MobileCamera MainCam;

    public static GameManager Instance { get; private set; }
    
    private void Start()
    {
        //string path = Application.streamingAssetsPath + @"/Database/Item_Database.json";
        //FileTool.TEST(path);
        //Storage.Instance.LoadInventory();
        LoadPlayerHouseData();
        NodeManager.Instance.LoadNodeData();
        WorkerManager.Instance.LoadWorkerData();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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

    public void LoadPlayerHouseData()
    {
        CityManager.Instance.LoadPlayerHouseData();
    }

    public void btn()
    {
        //Storage.Instance.AddRandomItemToRandomSlot();
    }

    public void save()
    {
        SaveManager.SaveEverything();
        //Storage.Instance.SaveInventory();
    }

    public void load()
    {
        //Storage.Instance.LoadInventory();
    }
}
