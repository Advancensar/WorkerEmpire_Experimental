using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SOItem))]
public class SOItem_Inspector : Editor {

    public override void OnInspectorGUI()
    {
        Debug.Log(((SOItem)target).type.GetType());

        if (GUILayout.Button("Change type"))
        {
            ((SOItem)target).type = new Equippable();
        }
        base.OnInspectorGUI();
    }

}
