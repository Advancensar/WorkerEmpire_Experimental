using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class House : MonoBehaviour
{
    public List<HouseData> HouseDatas = new List<HouseData>()
    {
        new HouseData(Current:0, Max:0, ContributionPoint:0, Type:"Bank"),
        new HouseData(Current:0, Max:0, ContributionPoint:0, Type:"Lodging"),
        new HouseData(Current:0, Max:0, ContributionPoint:0, Type:"Blacksmith"),
        new HouseData(Current:0, Max:0, ContributionPoint:0, Type:"Jeweler"),
    };

    public string Address;
    public HouseType HouseType;

    private void Awake()
    {
        Address = transform.parent.parent.parent.name + "/" + transform.parent.name + "/" + gameObject.name;
        HouseSaveManager.Houses.Add(Address, this);
    }

    void Start()
    {
        foreach (var data in HouseDatas)
        {
            if (data.Current > 0)
            {
                var type = CustomUtilities.GetType(data.Type);
                var obj = (HouseType)Activator.CreateInstance(type);
                Debug.Log("Current house type is : " + type);
            }
        }        
    }

    public PlayerHouseData GetHouseData()
    {
        var houseData = new PlayerHouseData();

        for (int i = 0; i < HouseDatas.Count; i++)
        {
            if (HouseDatas[i].Current > 0)
            {
                houseData.Current = HouseDatas[i].Current;
                houseData.HouseType = HouseDatas[i].Type;
                return houseData;
            }
        }

        houseData.HouseType = "Empty";
        houseData.Current = 0;
        return houseData;
    }

    public HouseData GetHouseDataByName(string type)
    {
        for (int i = 0; i < HouseDatas.Count; i++)
        {
            if (HouseDatas[i].Type == type)
            {
                return HouseDatas[i];
            }
        }
        return new HouseData(-1, -1, 0, "");
    }


    void Build(string type)
    {
        int typeCount = 0;
        int index = 0;

        for (int i = 0; i < HouseDatas.Count; i++)
        {
            if (HouseDatas[i].Current > 0)
            {
                typeCount++;
                index = i;
            }
        }
        if (typeCount <= 1)
        {
            if (HouseDatas[index].Type == type && HouseDatas[index].Current < HouseDatas[index].Max)
            {
                HouseDatas[index].Current++;
            }
        }
    }

    void Sell(string Type)
    {
        foreach (var data in HouseDatas)
        {
            if (data.Type == Type)
            {
                data.Current = 0;
                HouseType = null;
                break;
            }
        }
    }



}
