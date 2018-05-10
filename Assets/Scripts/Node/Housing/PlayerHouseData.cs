using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerHouseData
{
    public int Current;
    public string HouseType;

    public PlayerHouseData()
    {
        Current = 0;
        HouseType = "Empty";
    }

    public PlayerHouseData(int current, string houseType)
    {
        Current = current;
        HouseType = houseType;
    }
}
