using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftWindow : MonoBehaviour
{
    public GameObject CraftComponentPrefab;
    public GameObject Content;
    public Button Button_Craft;
    public TMP_InputField QuantityInputField;
    public Inventory Inventory;
    public Item ItemToCraft;
    public bool CanCraft = true; // Set this value to true everytime the window needs refreshing(after item craft)
    
    public void LoadWindowInfo(int itemID)
    {
        LoadWindowInfo(ItemDatabase.Instance.GetItem(itemID));
    }

    public void LoadWindowInfo(Item item)
    {
        gameObject.SetActive(true);
        ClearContent();
        ItemToCraft = item;
        foreach (var component in item.Recipe.List)
        {
            var craftComponent = Instantiate(CraftComponentPrefab, Content.transform).GetComponent<CraftComponent>();
            //Debug.Log("Craftid = " + component.id);
            craftComponent.LoadInfo(component.id, component.required);

        }
    }

    private void ClearContent()
    {
        foreach (Transform child in Content.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void CraftButton()
    {
        CanCraft = true;
        var RequiredComponents = new Dictionary<int, List<ItemObjectData>>();


        foreach (var component in ItemToCraft.Recipe.List)
        {
            if (GetCount(component.id) < component.required)
            {
                CanCraft = false;
                return;
            }
            var iodList = new List<ItemObjectData>();
            int requiredAmount = component.required;
            Debug.Log("Required Amount : " + requiredAmount);
            foreach (var slotNumber in Inventory.Items.Keys.ToList())
            {
                if (Inventory.Items[slotNumber].item.ID == component.id)
                {
                    if (requiredAmount <= 0) continue;
                    iodList.Add(Inventory.Items[slotNumber]);
                    requiredAmount--;
                }
            }

            RequiredComponents.Add(component.id, iodList);
        }

        int i = 0;
        foreach (var compID in RequiredComponents.Keys.ToList())
        {
            foreach (var iod in RequiredComponents[compID])
            {

                if (iod.item.HasItemType("Stackable"))
                {
                    iod.Consume(ItemToCraft.Recipe.List[i].required);
                    //iod.item.GetStackable().CurrentStack -= item.Recipe.List[i].required;
                }
                else
                {
                    iod.Consume();
                    
                }
            }

            i++;
        }

        //InventoryManager.Instance.GetInventoryByName("Heidel").AddItem(InventoryManager.Instance.GetInventoryByName("Heidel").FirstAvailableSlot(ItemToCraft));
        InventoryManager.Instance.GetInventoryByName("Heidel").AddItem(ItemToCraft);
        LoadWindowInfo(ItemToCraft);
        if (GameManager.Instance.InventoryWindow.CurrentInventory != null)
        {
            GameManager.Instance.InventoryWindow.LoadWindowInfo(InventoryManager.Instance.GetInventoryByName("Heidel"));
        }


    }

    void Craft()
    {

    }

    public int GetCount(Item item)
    {
        return GetCount(item.ID);
    }

    public int GetCount(int itemID)
    {
        GameManager.Instance.CraftWindow.Inventory = InventoryManager.Instance.Inventories["Heidel"];
        var Items = GameManager.Instance.CraftWindow.Inventory.Items;
        int count = 0;
        foreach (var slotNumber in Items.Keys.ToList())
        {
            if (Items[slotNumber].item.ID == itemID)
            {
                if (Items[slotNumber].item.HasItemType("Stackable"))
                {
                    count += Items[slotNumber].item.GetStackable().CurrentStack;

                }
                else
                {
                    count++;
                }
            }
        }
        return count;
    }



    bool fCanCraft(Item item)
    {
        foreach (var component in item.Recipe.List)
        {
            if (ItemDatabase.Instance.GetItem(component.id).HasItemType("Stackable"))
            {
                foreach (var slotNumber in Inventory.Items.Keys.ToList())
                {
                    if (Inventory.Items[slotNumber].item.ID == component.id)
                    {
                        return Inventory.Items[slotNumber].item.GetStackable().CurrentStack > component.required;
                    }

                }
            }


        }

        return false;
    }


}
