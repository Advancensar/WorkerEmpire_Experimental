using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    public Inventory Inventory;

    private void Awake()
    {
        Inventory.InventoryName = gameObject.name;
        InventoryManager.Instance.AddInventoryToInventoryManager(Inventory);
        InventoryManager.Instance.LoadInventory(Inventory);
        //Inventory.InventorySize = transform.Find("Viewport").Find("Content").childCount;
        //Inventory.CreateInventory(gameObject.name);
        //Inventory.LoadInventory();
    }

}
