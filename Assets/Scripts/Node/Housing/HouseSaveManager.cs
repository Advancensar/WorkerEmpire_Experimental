using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerHouseData
{
    public int Current;
    public string HouseType;
}

public class HouseSaveManager
{
    private static string PATH = @"/Database/HouseSavaData.json";

    public static Dictionary<string, House> Houses = new Dictionary<string, House>();
    private static Dictionary<string, PlayerHouseData> playerHouseData = new Dictionary<string, PlayerHouseData>();

    public static void AddPlayerHouseData(string address, PlayerHouseData info)
    {
        playerHouseData.Add(address, info);
    }

    public static void SavePlayerHouseData()
    {
        ClearPlayerHouseData();
        foreach (var key in Houses.Keys)
        {
            PlayerHouseData playerHouseData = new PlayerHouseData();
            playerHouseData = Houses[key].GetHouseData();
            if (playerHouseData.Current > 0)
            {
                AddPlayerHouseData(key, playerHouseData);
            }
        }
        FileTool.SaveFileAsJson(PATH, playerHouseData);
    }

    public static void LoadPlayerHouseData()
    {
        playerHouseData = FileTool.LoadObjectFromJson<Dictionary<string, PlayerHouseData>>(PATH);

        foreach (var key in playerHouseData.Keys)
        {
            var house = Houses[key];
            for (int i = 0; i < house.HouseDatas.Count; i++)
            {
                if (house.HouseDatas[i].Type == playerHouseData[key].HouseType)
                {
                    house.HouseDatas[i].Current = playerHouseData[key].Current;
                }
            }
        }
    }

    public static void ClearPlayerHouseData()
    {
        playerHouseData.Clear();
    }

}
