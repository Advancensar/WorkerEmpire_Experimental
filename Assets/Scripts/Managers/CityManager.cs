using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CityManager
{
    public static CityManager Instance { get; } = new CityManager();

    public Dictionary<string, CityNode> Cities = new Dictionary<string, CityNode>();
    
    private const string PATH = @"/Database/HouseSaveData.json";


    public void SavePlayerHouseData()
    {
        Dictionary<string, PlayerHouseData> tempPHD = new Dictionary<string, PlayerHouseData>();

        foreach (var cityName in Cities.Keys.ToList())
        {
            foreach (var houseAdress in Cities[cityName].Houses.Keys.ToList())
            {
                var phd = Cities[cityName].Houses[houseAdress].PlayerHouseData;
                if (phd.Current > 0)
                {
                    tempPHD.Add(houseAdress,phd);
                }
            }
        }

        FileTool.SaveFileAsJson(PATH, tempPHD);
    }

    public void LoadPlayerHouseData()
    {
        var tempPHD = FileTool.LoadObjectFromJson<Dictionary<string, PlayerHouseData>>(PATH);
        if (tempPHD == null)
            return;

        foreach (var address in tempPHD.Keys.ToList())
        {
            //Debug.Log(address.Split('/')[0]);
            Instance.Cities[address.Split('/')[0]].Houses[address].PlayerHouseData = tempPHD[address];
            //Debug.Log(address);
        }
    }


    //public static void SavePlayerHouseData()
    //{
    //    ClearPlayerHouseData();
    //    foreach (var key in GameManager.Instance.Houses.Keys.ToList())
    //    {   
    //        PlayerHouseData playerHouseData;
    //        playerHouseData = GameManager.Instance.Houses[key].GetHouseData();
    //        if (playerHouseData.Current > 0)
    //        {
    //            AddPlayerHouseData(key, playerHouseData);
    //        }
    //    }
    //    FileTool.SaveFileAsJson(PATH, playerHouseData);
    //}

    //public static void LoadPlayerHouseData()
    //{
    //    playerHouseData = FileTool.LoadObjectFromJson<Dictionary<string, PlayerHouseData>>(PATH);

    //    foreach (var key in playerHouseData.Keys.ToList())
    //    {
    //        var house = GameManager.Instance.Houses[key];
    //        foreach (var houseData in house.HouseDatas)
    //        {
    //            if (houseData.Type == playerHouseData[key].HouseType)
    //            {
    //                houseData.Current = playerHouseData[key].Current;
    //            }
    //        }
    //    }
    //}

    //public static void ClearPlayerHouseData()
    //{
    //    playerHouseData.Clear();
    //}

}
