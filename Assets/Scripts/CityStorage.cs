using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityStorage : MonoBehaviour {

    public Inventory Inventory;

    private void Start()
    {
        Inventory.InventoryName = gameObject.name;
        InventoryManager.Instance.AddInventoryToSaveManager(Inventory);
        InventoryManager.Instance.LoadInventory(Inventory);
    }
    

}
