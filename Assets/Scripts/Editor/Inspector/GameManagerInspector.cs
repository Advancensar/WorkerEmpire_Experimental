using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Reflection;
using System;
using System.Linq;

[CustomEditor(typeof(GameManager))]
public class GameManagerInspector : Editor
{
    
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        if (GUILayout.Button("Save Houses"))
        {
            CityManager.Instance.SavePlayerHouseData();
            //var houseHolderTransform = ((GameManager)target).HouseObjectsHolder.transform;
            //foreach (Transform go in houseHolderTransform)
            //{
            //    var house = go.GetComponent<House>();
            //    HouseSaveData.Instance.AddHouse(house.Adress, house.PlayerHouseInfo);
            //    Debug.Log("House : " + HouseSaveData.Instance.Data[house.Adress]);

            //}
            //FileTool.SaveFileAsJson(@"/Database/PlayerHouseData.json", HouseSaveData.Instance.Data);

            //HouseDatabase.Instance.SaveDB();
        }
    }





}
