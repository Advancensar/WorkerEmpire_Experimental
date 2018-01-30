using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Armor : BaseItemType
{
    public int Durability;

    public Armor()
    {
        Debug.Log("This is an Armor");
    }
}
