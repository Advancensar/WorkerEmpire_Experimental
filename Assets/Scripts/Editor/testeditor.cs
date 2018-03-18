using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(test))]
public class testeditor : Editor {

    Item tempItem = new Item();
    private bool isArmor = false;
    private bool isConsumable = false;
    private Dictionary<string, ItemType> tempTypes = new Dictionary<string, ItemType>();
    string filePath = "/Resources/Database/Storage.json";
    public void OnEnable()
    {
        filePath = Application.dataPath + filePath;
        //Storage.Items = FileTool.LoadObjectFromJson<List<Item>>(filePath);
    }
    public override void OnInspectorGUI()
    {
        //CustomSerializer.SerializeObject(new Storage());
        isArmor = EditorGUILayout.Toggle("Armor", isArmor);
        isConsumable = EditorGUILayout.Toggle("Consumable", isConsumable);

                
        if (isArmor)
        {
            if (!tempTypes.ContainsKey(typeof(Armor).ToString()))
            {
                tempTypes.Add(typeof(Armor).ToString(), new Armor());
            }          
        }
        if (isConsumable)
        {
            if (!tempTypes.ContainsKey(typeof(Consumable).ToString()))
            {
                tempTypes.Add(typeof(Consumable).ToString(), new Consumable());
            }
        }
        tempItem.Type = tempTypes;

        CustomSerializer.SerializeObject(tempItem);
        //if (GUILayout.Button("Add item to storage inventory"))
        //{
        //    Storage.Items.Add(tempItem);
        //    FileTool.SaveFileAsJson(filePath, Storage.Items);
        //}

        //if (GUILayout.Button("Erenmon Items"))
        //{
        //    foreach (var item in Storage.Items)
        //    {
        //        var go = (GameObject)Instantiate(Resources.Load("Prefabs/ItemObject")) as GameObject;
        //        go.GetComponent<ItemObject>().item = item;
        //        go.transform.parent = GameObject.Find("Slot").transform;
        //        go.GetComponent<RectTransform>().localPosition = Vector3.zero;
        //        //go.GetComponent<RectTransform>().localScale = Vector3.one;
        //    }
        //}
    }
}
