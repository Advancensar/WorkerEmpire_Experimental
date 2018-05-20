using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class HouseWindow : MonoBehaviour
{
    public GameObject HouseUsagePrefab;
    public GameObject BuyButton;
    public TextMeshProUGUI Address;

    public GameObject SelectedUsage;

    public Color prefabcolor; // temp

    string AssetPath = @"Sprites/UI/House";
    House house;
    Image Banner;

    Transform UsageContent;

    void Start()
    {
        prefabcolor = HouseUsagePrefab.GetComponent<Image>().color;
        UsageContent = transform.Find("Scroll View").Find("Viewport").Find("Content");
        gameObject.SetActive(false);
    }

    public void LoadWindowInfo(House house)
    {
        SelectedUsage = null;

        if (UsageContent != null)
            ClearContent();

        this.house = house;
        //var houseData = house.GetHouseData();
        Banner = Resources.Load<Image>(AssetPath + "/" + house.PlayerHouseData.HouseType);
        Address.text = house.Address;

        //Load data for each usage
        foreach (var data in house.HouseDatas)
        {
            if (data.Max > 0)
            {
                var Usage = Instantiate(HouseUsagePrefab, UsageContent, worldPositionStays: false);
                Usage.GetComponent<Button>().onClick.AddListener(OnSelectedChanged);

                Usage.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = data.Type;
                Usage.name = data.Type;
                test(Usage);
            }
        }
        RefreshButton();
    }

    void test(GameObject Usage)
    {
        var level = Usage.transform.Find("Level");
        var data = house.GetHouseDataByType(Usage.name);

        for (int i = 0; i < level.childCount; i++)
        {
            var child = level.GetChild(i);
            child.GetComponent<Image>().color = Color.white;
            child.gameObject.SetActive(false);

            if (data.Max > 0 && data.Max - 1 >= i)
            {
                child.gameObject.SetActive(true);

                if (data.Type == house.PlayerHouseData.HouseType)
                {
                    if (house.PlayerHouseData.Current > 0 && house.PlayerHouseData.Current - 1 >= i)
                    {
                        child.GetComponent<Image>().color = Color.green;
                    }
                }

            }

        }
    }


    void ClearContent()
    {
        foreach (Transform child in UsageContent)
        {
            Destroy(child.gameObject);
        }
    }


    public void OnSelectedChanged()
    {
        if (SelectedUsage != null)
        {
            SelectedUsage.GetComponent<Image>().color = prefabcolor;
        }
        SelectedUsage = EventSystem.current.currentSelectedGameObject;
        var type = SelectedUsage.GetComponentInChildren<TextMeshProUGUI>().text;
        SelectedUsage.GetComponent<Image>().color = Color.red;
        
        //SelectedUsage = house.GetHouseDataByType(type);
        RefreshButton();
    }

    private void RefreshButton()
    {
        if (SelectedUsage == null)
        {
            return;
        }
        if (house.GetHouseDataByType(SelectedUsage.name).Type == house.PlayerHouseData.HouseType)
        {
            BuyButton.GetComponentInChildren<TextMeshProUGUI>().text = "UPGRADE";

        }
        else
        {
            BuyButton.GetComponentInChildren<TextMeshProUGUI>().text = "BUY";
        }
        //if (house.PlayerHouseData.Current == 0)
        //{
        //    //BuyButton.SetActive(true);
        //    BuyButton.GetComponentInChildren<TextMeshProUGUI>().text = "BUY";
        //    //button text = buy
        //    //Disable sell button
        //}
        //else if (house.PlayerHouseData.Current < SelectedUsage.Max)
        //{
        //    //BuyButton.SetActive(true);
        //    if (house.PlayerHouseData.HouseType == SelectedUsage.Type)
        //    {
        //        BuyButton.GetComponentInChildren<TextMeshProUGUI>().text = "UPGRADE";
        //        //Button text = upgrade
        //    }
        //    else
        //    {
        //        //Button text = change usage
        //    }
        //}
        //else
        //{
        //    //BuyButton.SetActive(false);
        //    //disable buy button
        //}

        //switch (SelectedUsage.Type)
        //{
        //    case "Storage":
        //        //Window info = storage info

        //        break;
        //    default:
        //        break;
        //}

    }

    public void BuyButtonClick()
    {
        if (SelectedUsage == null) return;

        //if (EditorUtility.DisplayDialog("Title", "Message", "OK", "Cancel"))
        //{
          house.Build(house.GetHouseDataByType(SelectedUsage.name).Type);
          foreach (Transform usage in UsageContent)
          {
              test(usage.gameObject);
          }
        //}

        RefreshButton();
    }

    void modalWindow(int windowID)
    {
        Debug.Log("modal click");
    }

    public void SellButtonClick()
    {
        if (SelectedUsage == null) return;

        house.Sell(house.GetHouseDataByType(SelectedUsage.name).Type);
        test(SelectedUsage);
        RefreshButton();

    }

    public void OpenWindow()
    {
        gameObject.SetActive(true);
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
    }

}
    