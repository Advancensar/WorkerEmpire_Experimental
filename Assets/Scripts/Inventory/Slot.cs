using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler 
{
    public GameObject HeldItem;

    static GameObject ItemInMotion;
    static GameObject OldSlot;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        // If currently there's an item in motion, motion means we are trying to move an item by clicking on it
        if (ItemInMotion != null)
        {
            // If the target slot is empty
            if (HeldItem == null) 
            {
                HeldItem = ItemInMotion;
                HeldItem.transform.SetParent(transform);
                ItemInMotion = null;
                //Clear the color of previous slot
                OldSlot.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
            // If the target slot is not empty
            // We do the item swap here
            else
            {
                if (OldSlot != gameObject)
                {
                    ItemInMotion.transform.parent = null;
                    HeldItem.transform.SetParent(OldSlot.transform);
                    ItemInMotion.transform.SetParent(gameObject.transform);
                }

                OldSlot.GetComponent<Image>().color = new Color32(255,255,255,255);
                ItemInMotion = null;
            }
        }
        // If there's an item in slot
        else if (HeldItem != null) 
        {
            // If there's no item in motion when we click on a slot with an item in it
            if (ItemInMotion == null) 
            {
                GetComponent<Image>().color = new Color32(50, 0, 0, 120);
                ItemInMotion = HeldItem;
                OldSlot = gameObject;
            }
        }
    }
    
}
