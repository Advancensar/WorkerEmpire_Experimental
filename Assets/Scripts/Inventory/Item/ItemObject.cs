using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour
{

    public ItemObjectData ItemObjectData = new ItemObjectData();

    private void Awake()
    {
        //gameObject.name = itemObjectData.item.Name;
        //var img = GetComponent<Image>();
        //img.sprite = itemObjectData.item.Image();

        //GetComponent<Image>().sprite = test;


        //Debug.Log(item.Image());
        //Debug.Log("Test: " + ItemDatabase.Instance.Items);
    }

    public void LoadItemInfo()
    {
        gameObject.name = ItemObjectData.item.Name;
        var img = GetComponentInChildren<Image>();
        img.sprite = ItemObjectData.item.Image();
        RefreshGUI();
    }

    public void RefreshGUI()
    {
        if (ItemObjectData.item.HasItemType("Stackable"))
        {
            transform.Find("StackSize").GetComponent<TextMeshProUGUI>().text =
                ItemObjectData.item.GetStackable().CurrentStack.ToString();
        }
    }
}