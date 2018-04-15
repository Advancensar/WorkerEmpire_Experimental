using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour {

    Slot Slot;

    private void Start()
    {
        Slot = gameObject.GetComponent<Slot>();
    }

    private void Update()
    {
        if (Slot.HeldItem != null)
        {
            Debug.Log("Removed : " + Slot.HeldItem.name);

            Destroy(Slot.HeldItem);
        }
    }

}
