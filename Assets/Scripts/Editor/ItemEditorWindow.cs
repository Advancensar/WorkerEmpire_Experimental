using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemEditorWindow : EditorWindow {

    [MenuItem("Worker Empire/Item")]
    public static void ShowWindow()
    {
        GetWindow<ItemEditorWindow>("Item");
    }


    static Dictionary<int, bool> foldout = new Dictionary<int, bool>();
    Vector2 pos = new Vector2();

    void OnGUI()
    {
        pos = EditorGUILayout.BeginScrollView(pos);

        foreach (var item in ItemDatabase.Instance.Items)
        {
            int hashCode = item.GetHashCode();
            if (!foldout.ContainsKey(hashCode))
                foldout.Add(hashCode, false);

            //EditorGUILayout.BeginHorizontal();
            //GUILayout.Box(Resources.Load<Sprite>("Sprites/Items/" + item.Name).texture, GUILayout.Width(25), GUILayout.Height(25)); // Calling load all the time wtf?

            foldout[hashCode] = EditorGUILayout.Foldout(foldout[hashCode], string.Concat(item.ID, " : ", item.Name));
            //Resources.Load<Sprite>("Sprites/Items/" + Name);
            if (foldout[hashCode])
            {
                EditorGUILayout.BeginVertical();
                CustomInspector.SerializeObject(item);
                EditorGUILayout.EndVertical();
            }
            //EditorGUILayout.EndHorizontal();

        }
        EditorGUILayout.EndScrollView();

    }

}
