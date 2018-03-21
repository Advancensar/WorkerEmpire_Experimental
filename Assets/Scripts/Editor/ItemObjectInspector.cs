using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemObject))]

public class ItemObjectInspector : Editor {
    
    public override void OnInspectorGUI()
    {
        var temp = (ItemObject)target;
        CustomSerializer.SerializeObject(temp.itemObjectData);
        base.OnInspectorGUI();
    }

}
