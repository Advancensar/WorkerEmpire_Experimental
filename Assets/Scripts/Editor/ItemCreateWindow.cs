using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

public class ItemCreateWindow : EditorWindow
{
    private Item item = new Item();
    private Vector2 scrollPos;
    private Texture itemTexture;

    private Dictionary<string, bool> selectedToggles = new Dictionary<string, bool>()
    {
        { "Armor", false },
        { "Consumable", false },
        { "Equippable", false }
    };

    Dictionary<string, ItemType> selectedTypes = new Dictionary<string, ItemType>() { { "BaseType", new BaseType() } };
    
    [MenuItem("Worker Empire/Create Item")]
    private static void ShowWindow()
    {
        ItemCreateWindow window = GetWindow<ItemCreateWindow>();
        ItemDatabase.Instance.LoadDB();
    }

    private void OnGUI()
    {
        //EditorGUILayout.LabelField(ItemDatabase.Instance.Items.Count.ToString());

        AddSelectedTypesToItem();

        GUILayout.Box(itemTexture);

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        CustomInspector.SerializeObject(item);
        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("Create Item"))
        {
            ItemDatabase.Instance.Items = ItemDatabase.Instance.Items ?? new List<Item>();
            ItemDatabase.Instance.AddItem(item);
            ResetItem();
        }

        if (GUILayout.Button("Show Item Image"))
        {
            try
            {
                itemTexture = Resources.Load<Sprite>("Sprites/Items/" + item.Name).texture;
            }
            catch (Exception e)
            {
                itemTexture = null;
            }
        }

    }

    void AddSelectedTypesToItem()
    {
        item.Type = selectedTypes;
        foreach (var key in selectedToggles.Keys.ToList())
        {
            selectedToggles[key] = EditorGUILayout.Toggle(key, selectedToggles[key]);
        }

        foreach (var key in selectedToggles.Keys)
        {
            if (selectedToggles[key])
            {
                if (!selectedTypes.ContainsKey(key))
                {
                    selectedTypes.Add(key, (ItemType)Activator.CreateInstance(CustomUtilities.GetType(key)));
                }
            }
            else
            {
                if (selectedTypes.ContainsKey(key))
                {
                    selectedTypes.Remove(key);                    
                }
            }
        }

    }

    void ResetItem()
    {
        item = new Item();
        selectedTypes = new Dictionary<string, ItemType>() { { "BaseType", new BaseType() } };
    }
}
