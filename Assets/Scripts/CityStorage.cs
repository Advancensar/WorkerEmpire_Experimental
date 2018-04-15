using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CityStorage : MonoBehaviour {

    public Inventory Inventory;

    private void Awake()
    {
        Inventory.CreateInventory(gameObject.name);
        Inventory.LoadInventory();
    }
    

}
