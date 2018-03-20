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
    private Item tempItem = new Item();
    private Dictionary<string, ItemType> tempTypes = new Dictionary<string, ItemType>();
    private bool isArmor = false;
    private bool isConsumable = false;
    private bool isEquippable = false;

    private void OnEnable()
    {
        tempTypes.Add(typeof(BaseType).ToString(), new BaseType());
    }

    public override void OnInspectorGUI()
    {
        //EditorUtility.SerializeObject(tempItem);

        isArmor = EditorGUILayout.Toggle("Armor", isArmor);
        isConsumable = EditorGUILayout.Toggle("Consumable", isConsumable);
        isEquippable = EditorGUILayout.Toggle("Equippable", isEquippable);


        // Item Creation Type settings?
        #region Automate this next!
        if (isArmor)
        {
            if (!tempTypes.ContainsKey(typeof(Armor).ToString()))
            {
                tempTypes.Add(typeof(Armor).ToString(), new Armor());
            }
            else
            {
                //tempTypes.Remove(typeof(Armor).ToString());
            }
            #region Reflection trials
            //var fi = tempTypes["Armor"].GetType().GetFields(/*BindingFlags.Public |
            //                                                BindingFlags.NonPublic |
            //                                                BindingFlags.Instance*/);

            //foreach (var fi in tempTypes["Armor"].GetType().GetFields())
            //{
            //    //Debug.Log(fi.FieldType);
            //    if (fi.FieldType == typeof(int))
            //    {
            //        int tempInt = (int)fi.GetValue(tempTypes["Armor"]);
            //        tempInt = EditorGUILayout.IntField(fi.Name + " : ", tempInt);
            //        fi.SetValue(tempTypes["Armor"], tempInt);
            //    }
            //    else if (fi.FieldType == typeof(string))
            //    {
            //        string tempString = fi.GetValue(tempTypes["Armor"]).ToString();
            //        tempString = EditorGUILayout.TextField(fi.Name + " : ", tempString);
            //        fi.SetValue(tempTypes["Armor"], tempString);
            //    }
            //    //Debug.Log(fi.GetValue(tempTypes["Armor"]));

            //    //if (fi.GetValue(tempTypes["Armor"]).GetType() == typeof(int))
            //    //{
            //    //    Debug.Log("asdasd");
            //    //}
            //    //if (fi.GetType() == )
            //    //{
            //    //    Debug.Log(fi);
            //    //}
            //}
            //((Armor)tempTypes["Armor"]).test = EditorGUILayout.TextField("NOT AUTO Test : ", ((Armor)tempTypes["Armor"]).test);                               //
            //((Armor)tempTypes["Armor"]).Durability = EditorGUILayout.IntField("NOT AUTO Durability : ", ((Armor)tempTypes["Armor"]).Durability);              //
            //                                                                                                                                         // Trying to replace this with automated system.reflection
            //tempItem.Type[((Armor)tempTypes["Armor"]).GetType().ToString()] = ((Armor)tempTypes["Armor"]);                                           // methods
            #endregion
        }
        if (isConsumable)
        {
            if (!tempTypes.ContainsKey(typeof(Consumable).ToString()))
            {
                tempTypes.Add(typeof(Consumable).ToString(), new Consumable());
            }
            else
            {
                //tempTypes.Remove(typeof(Consumable).ToString());
            }
        }

        if (isEquippable)
        {
            if (!tempTypes.ContainsKey(typeof(Equippable).ToString()))
            {
                tempTypes.Add(typeof(Equippable).ToString(), new Equippable());
            }
            else
            {
                //tempTypes.Remove(typeof(Consumable).ToString());
            }
        }
        #endregion

        CustomSerializer.SerializeObject(tempItem);
        
        // Show fields for each type
        foreach (var type in tempTypes.Values)
        {
            //CustomSerializer.SerializeObject(type);

            //EditorUtility.SerializeObject(type);
        }

        // Set tempItem types to current types
        tempItem.Type = tempTypes;
        
        #region Buttons
        if (GUILayout.Button("Add item / save db"))
        {
            ItemDatabase.Instance.AddItem(tempItem);
            tempItem = new Item();
            tempTypes = new Dictionary<string, ItemType>();
            tempTypes.Add(typeof(BaseType).ToString(), new BaseType());

            ItemDatabase.Instance.SaveDB();
        }

        if (GUILayout.Button("Load DB"))
        {
            ItemDatabase.Instance.LoadDB();
        }

        if (ItemDatabase.Instance.Items != null)
        {
            foreach (var item in ItemDatabase.Instance.Items)
            {
                //GUILayout.Label(item.Name, EditorStyles.boldLabel);
                //EditorGUI.indentLevel++;
                //EditorUtility.SerializeObject(item);
                foreach (var type in item.Type.Values)
                {
                    //EditorGUILayout.LabelField(type.ToString(), EditorStyles.boldLabel);
                    //EditorUtility.SerializeObject(type);
                    //CustomSerializer.SerializeObject(type);
                    //GUILayout.TextField(type.ToString());
                }
            }
        }

        if (GUILayout.Button("Clear db"))
        {
            ItemDatabase.Instance.Items.Clear();
            ItemDatabase.Instance.SaveDB();
        }
        #endregion
        base.OnInspectorGUI();
    }

    



}
