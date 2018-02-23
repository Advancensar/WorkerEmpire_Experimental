using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    [Header("-- Default Inspector --")]
    public int ID;
    public string Name = "";
    public Dictionary<string, ItemType> Type = new Dictionary<string, ItemType>();

}
