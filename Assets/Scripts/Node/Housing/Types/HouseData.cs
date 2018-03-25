using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HouseData
{
    public int Current;
    public int Max;
    public int ContributionPoint;
    public string Name;

    public HouseData(int Current, int Max, int ContributionPoint, string Name)
    {
        this.Current = Current;
        this.Max = Max;
        this.ContributionPoint = ContributionPoint;
        this.Name = Name;

    }

}
