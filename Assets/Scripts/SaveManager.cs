using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager {

    public static void SaveEverything()
    {
        SaveHouseData();
        SavePlayerInventories();
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

}
