using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltipWindow : MonoBehaviour
{
    public ItemObject ItemObject;
    public Transform TrashCan;
    public TextMeshProUGUI PriceText;
    public GameObject ConsumeButton;

    int stackSize;

    public void LoadWindowInfo(ItemObject itemObject)
    {
        ItemObject = itemObject;

        ConsumeButton.SetActive(ItemObject.ItemObjectData.item.HasItemType("Consumable"));

        stackSize = itemObject.ItemObjectData.item.HasItemType("Stackable") ? ItemObject.ItemObjectData.item.GetStackable().CurrentStack : 1;

        PriceText.text = (itemObject.ItemObjectData.item.Price * stackSize).ToString();

    }

    public void Sell()
    {
        PlayerManager.Instance.Gold += (ItemObject.ItemObjectData.item.Price * stackSize);
        RemoveFromInventory();
    }

    public void Consume()
    {
        ((Consumable) ItemObject.ItemObjectData.item.GetTypeByName("Consumable")).Consume();
        RemoveFromInventory();
    }

    void RemoveFromInventory()
    {
        InventoryManager.Instance.GetInventoryByName(ItemObject.ItemObjectData.InventoryName)
            .RemoveItem(ItemObject.ItemObjectData.SlotNumber);
        GameManager.Instance.InventoryWindow.LoadWindowInfo(
            InventoryManager.Instance.GetInventoryByName(ItemObject.ItemObjectData.InventoryName));
        GameManager.Instance.InventoryWindow.Slots[ItemObject.ItemObjectData.SlotNumber].GetComponent<Image>().color =
            new Color32(255, 255, 255, 255);
    }

}
