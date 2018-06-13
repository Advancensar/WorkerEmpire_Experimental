using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stackable : ItemType
{
    public int CurrentStack = 1;
        
    public void AddToStack(int size)
    {
        CurrentStack += size;
    }
}
