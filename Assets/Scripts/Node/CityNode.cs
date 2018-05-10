using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityNode : Node
{
    public Dictionary<string, House> Houses = new Dictionary<string, House>();
    //private Dictionary<string, PlayerHouseData> playerHouseData = new Dictionary<string, PlayerHouseData>();


    void Awake()
    {
        CityManager.Instance.Cities.Add(gameObject.name, this);
    }
    void asd()
    {

    }


}
