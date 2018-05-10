using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HouseData
{
    public int Max;
    public int ContributionPoint;
    public string Type;

    public HouseData(int Max, int ContributionPoint, string Type)
    {
        this.Max = Max;
        this.ContributionPoint = ContributionPoint;
        this.Type = Type;
    }

}
