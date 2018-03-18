using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Newtonsoft.Json;
using System.Reflection;

//[CustomEditor(typeof(ItemObject))]
public class ItemInspector : Editor {

    Item item;

    public override void OnInspectorGUI()
    {
        item = ((ItemObject)target).itemObjectData.item;

        if (GUILayout.Button("Add"))
        {
            Undo.RecordObject(target, target.name);
            item.Name = "Erenmon";
            //item.Type.Add(new Armor());

        }
        if (GUILayout.Button("Show Info"))
        {
            var assembly = Assembly.GetAssembly(typeof(SceneView));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);

            Debug.Log("Path is : " + Application.dataPath);

            foreach (var item in item.Type)
            {
                Debug.Log(item.ToString());
            }

        }
        if (GUILayout.Button("Clear List"))
        {
            item.Type.Clear();

        }
        if (GUILayout.Button("SerializeToJson"))
        {
            var jsonString = JsonConvert.SerializeObject(item, Formatting.Indented, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            Debug.Log(jsonString);

            var test = JsonConvert.DeserializeObject<Item>(jsonString, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
            Debug.Log(test);
            //item.Type.Add(test.Type[0]);
        }

        //Default Inspector
        base.OnInspectorGUI();
    }

}
