using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager {

    public static void SaveEverything()
    {
        SaveHouseData();
        SavePlayerInventories();
        SaveNodes();
        SaveWorkers();
        SavePlayer();
    }
    
    public static void SaveHouseData()
    {
        CityManager.Instance.SavePlayerHouseData();
    }
    public static void LoadHouseData()
    {
        CityManager.Instance.LoadPlayerHouseData();
    }
    public static void SavePlayerInventories()
    {
        InventoryManager.Instance.SaveAllInventories();
    }
    public static void LoadPlayerInventories()
    {
        InventoryManager.Instance.LoadAllInventories();
    }
    public static void SaveNodes()
    {
        NodeManager.Instance.SaveNodeData();
    }
    public static void LoadNodes()
    {
        NodeManager.Instance.LoadNodeData();
    }
    public static void SaveWorkers()
    {
        WorkerManager.Instance.SaveWorkerData();
    }
    public static void LoadWorkers()
    {
        WorkerManager.Instance.LoadWorkerData();
    }
    public static void SavePlayer()
    {
        PlayerManager.Instance.SavePlayerData();
    }
    public static void LoadPlayer()
    {
        PlayerManager.Instance.LoadPlayerData();
    }

}
