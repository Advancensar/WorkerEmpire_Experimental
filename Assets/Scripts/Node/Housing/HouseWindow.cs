using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class HouseWindow : MonoBehaviour {

    public GameObject HouseUsagePrefab;

    string AssetPath = @"Sprites/UI/House";
    House house;
    Image Banner;

    Transform UsageContent;

    public void LoadWindowInfo(House house)
    {
        if (UsageContent != null)
            ClearContent();

        this.house = house;
        var houseData = house.GetHouseData();
        Banner = Resources.Load<Image>(AssetPath + houseData.HouseType);

        UsageContent = transform.Find("Scroll View").Find("Viewport").Find("Content");

        //Load data for each usage
        foreach (var data in house.HouseDatas)
        {
            if (data.Max > 0)
            {
                var Usage = Instantiate(HouseUsagePrefab, UsageContent, worldPositionStays: false);
                Usage.GetComponent<Button>().onClick.AddListener(OnSelectedChanged);

                Usage.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = data.Type;

                var level = Usage.transform.Find("Level");

                for (int i = 0; i < level.childCount; i++)
                {
                    var child = level.GetChild(i);
                    child.gameObject.SetActive(false);

                    if (data.Max > 0 && data.Max-1 >= i)
                    {
                        child.gameObject.SetActive(true);

                        if (data.Current > 0 && data.Current-1 >= i)
                        {
                            level.GetChild(i).GetComponent<Image>().color = Color.red;
                        }
                    }

                }
            }
            //usage.transform.Find("Level");
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
        var Selected = EventSystem.current.currentSelectedGameObject;
        var type = Selected.GetComponentInChildren<TextMeshProUGUI>().text;
        Debug.Log(type);

        var data = house.GetHouseDataByName(type);

        if (data.Current == 0)
        {
            //button text = buy
            //Disable sell button
        }
        else if (data.Current < data.Max)
        {
            if (data.Type == house.GetHouseData().HouseType)
            {
                //Button text = upgrade
            }
            else
            {
                //Button text = change usage
            }
        }
        else
        {
            //disable buy button
        }

        switch (data.Type)
        {
            case "Storage":
                //Window info = storage info

                break;
            default:
                break;
        }

    }

}
    