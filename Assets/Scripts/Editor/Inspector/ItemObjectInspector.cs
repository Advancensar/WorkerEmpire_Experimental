using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(ItemObject))]

public class ItemObjectInspector : Editor {

    ItemTypes selectedType;    

    public override void OnInspectorGUI()
    {
        var centeredBoldLabel = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold };
        EditorGUILayout.LabelField("Edit item properties", centeredBoldLabel);        

        selectedType = (ItemTypes)EditorGUILayout.EnumPopup("Type:", selectedType);
        
        if (GUILayout.Button("Add Type"))
        {
            var type = CustomUtilities.GetType(selectedType.ToString());
            var obj = (ItemType)Activator.CreateInstance(type);

            ((ItemObject)target).ItemObjectData.item.Type.Add(type.ToString(), obj);
        }
        if (GUILayout.Button("Remove Type"))
        {
            var type = CustomUtilities.GetType(selectedType.ToString());
            ((ItemObject)target).ItemObjectData.item.Type.Remove(type.ToString());
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);        

        var temp = (ItemObject)target;

        //GUILayout.Box(temp.GetComponentInChildren<Image>().mainTexture);

        CustomInspector.SerializeObject(temp.ItemObjectData);
        base.OnInspectorGUI();
    }
    
}
