using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour {

    Slot Slot;

    private void Start()
    {
        Slot = gameObject.GetComponent<Slot>();
    }

    private void OnTransformChildrenChanged()
    {
        if (Slot.HeldItem != null)
        {
            if (transform.childCount > 1)
            {
                //Debug.Log("Removed : " + Slot.HeldItem.name);
                var iod = Slot.HeldItem.GetComponent<ItemObject>().ItemObjectData;
                Destroy(Slot.HeldItem);
                InventoryManager.Instance.GetInventoryByName(iod.InventoryName).RemoveItem(iod.SlotNumber);

            }

        }
    }    

}
