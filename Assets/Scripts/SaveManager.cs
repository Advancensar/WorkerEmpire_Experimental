using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager {
    
    public static Dictionary<string, Dictionary<int, ItemObjectData>> Inventory = new Dictionary<string, Dictionary<int, ItemObjectData>>();

    public static void SaveHouseData()
    {
        HouseSaveManager.SavePlayerHouseData();
    }
    public static void LoadHouseData()
    {
        HouseSaveManager.LoadPlayerHouseData();
    }
    public static void SavePlayerInventories()
    {
        InventorySaveManager.SaveAllInventories();
    }
    public static void LoadPlayerInventories()
    {
        InventorySaveManager.LoadAllInventories();
    }

}
