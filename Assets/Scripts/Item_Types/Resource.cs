using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Lumber,
    Metal,
    Ore
}

public class Resource : ItemType
{
    public int WorkTime;
}
