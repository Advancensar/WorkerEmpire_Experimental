using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Item))]
public class ItemInspector : Editor {

    Item item;

    public override void OnInspectorGUI()
    {
        item = (Item)target;

        if (GUILayout.Button("Add"))
        {
            item.Name = "Erenmon";
            item.Type.list.Add(new Armor());
        }
        if (GUILayout.Button("Show Info"))
        {
            foreach (var item in item.Type.list)
            {
                Debug.Log(item.ToString());
            }
            
        }
        foreach (var item in item.Type.list)
        {
            GUILayout.TextField(item.ToString());
        }

        //Default Inspector
        base.OnInspectorGUI();
    }

}
