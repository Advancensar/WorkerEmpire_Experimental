using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

[System.Serializable]
public class ItemDatabase
{
    private static ItemDatabase instance = null;
    private static readonly object padlock = new object();

    private string DATABASE_PATH = @"/Resources/Database/Item_Database.json";

    public List<Item> Items = new List<Item>();

    public static ItemDatabase Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ItemDatabase();
                    instance.DATABASE_PATH = Application.dataPath + instance.DATABASE_PATH;
                    instance.LoadDB();
                }
                return instance;
            }
        }
    }
 
    //public static ItemDatabase Instance
    //{
    //    get
    //    {
    //        if (instance == null) instance = new ItemDatabase();
    //        return instance;
    //    }
    //}

    private void Awake()
    {
        //Sets database object to not be destroyed when loading scene 
        //DontDestroyOnLoad(gameObject);

        //Debug.Log("Start " + Application.dataPath);
        //DATABASE_PATH = Application.dataPath + DATABASE_PATH;
        //Debug.Log(DATABASE_PATH);

        //if (Instance == null)
        //{
        //    //if not, set instance to this
        //    Instance = this;
        //    CreateNewInstance();
        //}


        //If instance already exists and it's not this:
        //else if (Instance != this)

        //    //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
        //    Destroy(gameObject);

        //if (Instance != null && Instance != this)
        //{
        //    Debug.Log("Instance is not null(?)");
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    Debug.Log("New db created");
        //    CreateNewInstance();
        //}
        
        //LoadDB();
    }    

    //private void CreateNewInstance()
    //{
    //    Debug.Log("Creating new instance");
    //    Instance = new ItemDatabase();
    //    Items = new List<Item>();
    //}

    public void LoadDB()
    {
        Debug.Log("Loading DB at path : " + DATABASE_PATH);
        if (File.Exists(DATABASE_PATH))
        {

            Items = JsonConvert.DeserializeObject<List<Item>>(File.ReadAllText(DATABASE_PATH),
                                                            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
        }
        //else
        //    CreateNewInstance();
    }

    public void SaveDB()
    {
        string jsonString = JsonConvert.SerializeObject(this.Items,
                                                        Formatting.Indented,
                                                        new JsonSerializerSettings
                                                        {
                                                            TypeNameHandling = TypeNameHandling.All,
                                                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                        });

        File.WriteAllText(DATABASE_PATH, jsonString);
        //if (File.Exists(DATABASE_PATH))
        //{
        //    File.WriteAllText(@DATABASE_PATH, jsonString);
        //    //JsonWriter jw = File.OpenText(DATABASE_PATH);
        //    using (StreamWriter file = File.OpenWrite(DATABASE_PATH))
        //    {
        //        JsonSerializer serializer = new JsonSerializer();
        //        serializer.Serialize(file, jsonString);
        //    }
        //}
    }

    public Item GetItem(string name)
    {
        foreach (var Item in Items)
        {
            if (Item.Name == name)
                return Item;
        }
        Debug.LogError("Couldn't find the item with the name : " + name);
        return null;
    }

    public Item GetItem(int ID)
    {
        foreach (var Item in Items)
        {
            if (Item.ID == ID)
                return Item;
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
    }

}
