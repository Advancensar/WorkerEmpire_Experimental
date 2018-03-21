using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : MonoBehaviour {

    public ItemObjectData itemObjectData = new ItemObjectData();

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
        gameObject.name = itemObjectData.item.Name;
        var img = GetComponentInChildren<Image>();
        img.sprite = itemObjectData.item.Image();
    }
}
