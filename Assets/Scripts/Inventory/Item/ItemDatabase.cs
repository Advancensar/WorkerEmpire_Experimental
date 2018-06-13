using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

public class ItemDatabase
{
    private static ItemDatabase instance = null;
    private static readonly object padlock = new object();

    private const string PATH = @"/Database/Item_Database.json";

    public List<Item> Items = new List<Item>();

    public static ItemDatabase Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ItemDatabase
                    {
                        Items = new List<Item>()
                    };
                    instance.LoadDB();
                }
                return instance;
            }
        }
    }

    public void LoadDB()
    {
        Items = FileTool.LoadObjectFromJson<List<Item>>(PATH);
    }

    public void SaveDB()
    {
        FileTool.SaveFileAsJson(PATH, Items);
    }

    public Item GetItem(string name)
    {
        foreach (var Item in Items)
        {
            if (Item.Name == name)
                return Clone(Item);
        }
        Debug.LogError("Couldn't find the item with the name : " + name);
        return null;
    }

    public Item GetItem(int ID)
    {
        foreach (var Item in Items)
        {
            //Debug.Log(Item.ID);
            if (Item.ID == ID)
                return Clone(Item);
        }
        Debug.LogError("Couldn't find the item with the ID : " + ID);
        return null;
    }

    public void AddItem(Item item)
    {
        Debug.Log("Adding item to list : " + item);
        if (Instance.Items == null)
        {
            Instance.Items = new List<Item>();
        }
        Instance.Items.Add(item);

        //SaveAddedItemToDB();
        SaveDB();
    }

    private void SaveAddedItemToDB()
    {
        //Parse the database file
        //Add to the end of the file instead of replacing the whole file? HOW tho?

    }

    public Item RandomItem()
    {
        return GetItem(Random.Range(0, Items.Count));
    }

    public static T Clone<T>(T source)
    {
        var serialized = JsonConvert.SerializeObject(source, Formatting.Indented,
            new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            }
        );
        return JsonConvert.DeserializeObject<T>(serialized,
            new JsonSerializerSettings {TypeNameHandling = TypeNameHandling.Auto});
    }
}
