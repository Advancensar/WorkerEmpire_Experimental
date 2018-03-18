using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageSlot : Slot {

    public int SlotNumber;

    void OnTransformChildrenChanged()
    {
        if (transform.childCount > 0)
        {
            HeldItem = transform.GetChild(0).gameObject;
            HeldItem.GetComponent<RectTransform>().localPosition = Vector2.zero;
            HeldItem.GetComponent<ItemObject>().itemObjectData.SlotNumber = SlotNumber;
            Storage.Items.Add(HeldItem.GetComponent<ItemObject>());
        }
        else
        {
            Storage.Items.Remove(HeldItem.GetComponent<ItemObject>());
            HeldItem = null;
        }
        
    }


}
