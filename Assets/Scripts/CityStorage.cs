using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityStorage : MonoBehaviour {

    public Inventory Inventory;

    private void Awake()
    {
        Inventory.InventoryName = gameObject.name;
        InventoryManager.Instance.AddInventoryToInventoryManager(Inventory);
        InventoryManager.Instance.LoadInventory(Inventory);
    }
    

}
