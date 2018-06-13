using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : ItemType
{
    public int ConsumableInt = 1235;
    public List<int> Effects = new List<int>() { 99, 12, 53, 22 };

    public void Consume()
    {
        foreach (var effect in Effects)
        {
            switch (effect)
            {
                case 0:
                    EnergyPot(10);
                    break;
                case 1:
                    EnergyPot(20);
                    break;
            }
        }
        Debug.Log("Consumed the item");
    }

    void EnergyPot(int Amount)
    {

    }
}
