using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ArmorWorkshop : HouseType
{
    //public List<int> Craftables = new List<int>() { 1,2,3,4,5 };

    public ArmorWorkshop()
    {
        Craftables = new List<int>() {1, 2, 3, 4, 5};
    }
}
