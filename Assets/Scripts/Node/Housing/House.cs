using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public List<HouseData> HouseDatas = new List<HouseData>()
    {
        new HouseData(Current:0,Max:0,ContributionPoint:0,Name:"Bank"),
        new HouseData(Current:0,Max:0,ContributionPoint:0,Name:"Lodging"),
        new HouseData(Current:0,Max:0,ContributionPoint:0,Name:"Blacksmith"),
        new HouseData(Current:0,Max:0,ContributionPoint:0,Name:"Jeweler"),
        new HouseData(Current:0,Max:0,ContributionPoint:0,Name:"Bank")
    };

    public HouseType HouseType;


    void Start()
    {
        foreach (var data in HouseDatas)
        {
            if (data.Current > 0)
            {
                var type = CustomUtilities.GetType(data.Name);
                var obj = (HouseType)Activator.CreateInstance(type);
                Debug.Log("Current house type is : " + type);
            }
        }
        
    }


    void asd(string type)
    {
        int a = 0;
        int index = 0;

        for (int i = 0; i < HouseDatas.Count; i++)
        {
            if (HouseDatas[i].Current > 0)
            {
                a++;
                index = i;
            }
        }
        if (a <= 1)
        {
            Build(type, index);
        }
    }

    void Build(string type, int index)
    {        
        if (HouseDatas[index].Name == type && HouseDatas[index].Current < HouseDatas[index].Max)
        {
            HouseDatas[index].Current++;
        }
        
    }

    void Sell(string Type)
    {
        foreach (var data in HouseDatas)
        {
            if (data.Name == Type)
            {
                data.Current = 0;
                HouseType = null;
                break;
            }
        }
    }



}
