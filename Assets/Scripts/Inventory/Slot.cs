using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SlotType
{
    None,
    MainHand,
    OffHand,
    Head,
    Body,
    Leg,
    Feet,
    Ring,
    Amulet
}

public class Slot : MonoBehaviour, IPointerClickHandler 
{
    public static Dictionary<int, GameObject> Slots = new Dictionary<int, GameObject>();
    public static int inventoryOffset = 0;

    public GameObject HeldItem;

    static GameObject ItemInMotion;
    static GameObject OldSlot;

    public SlotType SlotType;
    public int SlotNumber;

    private void Start()
    {
        //If it's player inventory.
        if (gameObject.tag == "CharacterItem")
        {
            //Set the storage inventory offset if it's not has been set.
            if (inventoryOffset == 0)
                inventoryOffset = transform.parent.childCount;

            SlotNumber = transform.GetSiblingIndex();
            Slots.Add(SlotNumber, gameObject);
        }
        //If it's storage inventory
        else
        {
            SlotNumber = (transform.GetSiblingIndex()) + inventoryOffset;
            Slots.Add(SlotNumber, gameObject);
        }
    }

    void OnTransformChildrenChanged()
    {
        if (transform.childCount > 0)
        {
            HeldItem = FirstChildWithItemObject();
            if (HeldItem != null)
            {
                HeldItem.GetComponent<RectTransform>().localPosition = Vector2.zero;
                HeldItem.GetComponent<ItemObject>().itemObjectData.SlotNumber = SlotNumber;
                Storage.Items.Add(HeldItem.GetComponent<ItemObject>());
            }
        }
        else
        {
            Storage.Items.Remove(HeldItem.GetComponent<ItemObject>());
            HeldItem = null;
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // If currently there's an item in motion, motion means we are trying to move an item by clicking on it
        if (ItemInMotion != null)
        {
            // If the target slot is empty
            if (HeldItem == null) 
            {
                if (CanMove(gameObject, ItemInMotion))
                {
                    HeldItem = ItemInMotion;
                    HeldItem.transform.SetParent(transform);
                    ItemInMotion = null;
                    //Clear the color of previous slot
                    OldSlot.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                }
            }
            // If the target slot is not empty
            // We do the item swap here
            else
            {
                if (OldSlot != gameObject)
                {
                    if (CanSwap(gameObject, OldSlot, ItemInMotion))
                    {
                        ItemInMotion.transform.parent = null;
                        HeldItem.transform.SetParent(OldSlot.transform);
                        ItemInMotion.transform.SetParent(gameObject.transform);
                    }
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

    //Check if the target and old slot is capable of swapping item in slot with item in motion.
    bool CanSwap(GameObject slot, GameObject oldSlot, GameObject item)
    {
        var slotType = slot.GetComponent<Slot>().SlotType;
        var oldSlotType = oldSlot.GetComponent<Slot>().SlotType;
        var itemSlotType = item.GetComponent<ItemObject>().itemObjectData.item.slotType();

        if (SlotType == SlotType.None && oldSlotType == SlotType.None)
            return true;
        if (SlotType == itemSlotType && oldSlotType == itemSlotType)
            return true;

        return false;
    }
    //Check if the target slot accepts the item in motion
    bool CanMove(GameObject slot, GameObject item)
    {
        var slotType = slot.GetComponent<Slot>().SlotType;

        if (SlotType == SlotType.None)
            return true;
        if (SlotType == item.GetComponent<ItemObject>().itemObjectData.item.slotType())
            return true;

        return false;
    }

    GameObject FirstChildWithItemObject()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "ItemObject")
            {
                return transform.GetChild(i).gameObject;
            }
        }
        return null;
    }

}
