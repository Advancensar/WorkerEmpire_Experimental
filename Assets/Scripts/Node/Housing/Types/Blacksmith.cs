using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Blacksmith : HouseType
{
    public Blacksmith()
    {
        Craftables = new List<int>() { 1, 2, 2, 2, 1, 2, 2, 2, 1, 2, 2, 2, 1, 2, 2, 2, 1 };
    }
}
