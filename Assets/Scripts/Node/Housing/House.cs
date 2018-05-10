using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class House : MonoBehaviour
{
    public List<HouseData> HouseDatas = new List<HouseData>()
    {
        new HouseData(Max:0, ContributionPoint:0, Type:"Bank"),
        new HouseData(Max:0, ContributionPoint:0, Type:"Lodging"),
        new HouseData(Max:0, ContributionPoint:0, Type:"Blacksmith"),
        new HouseData(Max:0, ContributionPoint:0, Type:"Jeweler"),
    };

    public PlayerHouseData PlayerHouseData = new PlayerHouseData();

    public string Address;
    public HouseType HouseType;

    private void Awake()
    {
        Address = transform.parent.parent.parent.name + "/" + transform.parent.name + "/" + gameObject.name;

    }

    private void Start()
    {
        CityManager.Instance.Cities[transform.parent.parent.parent.name].Houses.Add(Address, this);
        foreach (var data in HouseDatas)
        {
            if (PlayerHouseData.Current > 0)
            {
                var type = CustomUtilities.GetType(data.Type);
                HouseType = (HouseType)Activator.CreateInstance(type);
                break;
                //Debug.Log("Current house type is : " + type);
            }
        }
    }

    public HouseData GetHouseDataByType(string houseType)
    {
        foreach (var houseData in HouseDatas)
        {
            if (houseType == houseData.Type)
            {
                return houseData;
            }
        }

        return null;
    }

    public void Build(string type)
    {
        if (type != PlayerHouseData.HouseType && PlayerHouseData.Current > 0)
        {
            Sell(PlayerHouseData.HouseType);
        }

        if (PlayerHouseData.Current == 0)
        {
            PlayerHouseData.Current = 1;
            PlayerHouseData.HouseType = type;
        }
        else // upgrade
        {
            Upgrade();
        }

    }

    public void Upgrade()
    {
        PlayerHouseData.Current++;

    }

    public void Sell(string Type)
    {
        PlayerHouseData = new PlayerHouseData();

    }



}
