using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftObject : MonoBehaviour, IPointerClickHandler
{
    public Item item;

    public void LoadInfo()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = item.Image();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
        {
            GameManager.Instance.CraftWindow.LoadWindowInfo(item);
        }
    }
}
