using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftComponent : MonoBehaviour, IPointerClickHandler
{
    public Image Img;
    public TextMeshProUGUI Text;
    public Item Item;
    public int RequiredQuantity;

    private void Awake()
    {
        Img = transform.Find("Component_Image").GetComponent<Image>();
        Text = transform.Find("Component_Text").GetComponent<TextMeshProUGUI>();
        //Img = GetComponentsInChildren<Image>()[1];
        //Text = GetComponentInChildren<TextMeshProUGUI>();
        //LoadInfo(1, 1);
    }

    public void Start()
    {
        //LoadInfo(1, 2);

    }

    public void LoadInfo(int itemID, int requiredQuantity)
    {
        LoadInfo(ItemDatabase.Instance.GetItem(itemID), requiredQuantity);

    }
    public void LoadInfo(Item item, int requiredQuantity)
    {
        Item = item;
        Img.sprite = Item.Image();
        int countInInventory = GameManager.Instance.CraftWindow.GetCount(item);
        
        string textColor = countInInventory >= requiredQuantity ? "green" : "red";

        Text.text = "<color=" + textColor + ">" + countInInventory + "</color>/" + requiredQuantity;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Info");
    }
    
}
