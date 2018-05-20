using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 
{
    //public List<int> asdas = new List<int>() { 1, 2, 4 };
    //public string testString;//= "TestString";
    //public int testInt = 1;
    //public Color col = new Color();
    public Dictionary<string, int> asqweqwe = new Dictionary<string, int>()
    {
        {"erenmon", 12 },
        {"erenmon2", 12 }
    };
    public Dictionary<Item, string> itemstringdic = new Dictionary<Item, string>()
    {
        {new Item()
            {
            ID = 1,
            Name = "some random item",
            Type = new Dictionary<string, ItemType>()
            {
                {"Armor", new Armor()},
                {"Consumable", new Consumable()}
            }
        }, "itemstring" }
    };

    private Item item = new Item()
    {
        ID = 1,
        Name = "some random item",
        Type = new Dictionary<string, ItemType>()
        {
            {"Armor", new Armor()},
            {"Consumable", new Consumable()}
        }
    };

    public Dictionary<string, Dictionary<int, Item>> strintitemdic = new Dictionary<string, Dictionary<int, Item>>();

    public test2()
    {
        var qq = new Dictionary<int, Item>()
        {
            {1, new Item() {ID = 1, Name = "erenmon"}},
            {2, new Item() {ID = 2, Name = "erenmon1"}},
            {3, new Item() {ID = 3, Name = "erenmon2"}},
            {
                4, new Item()
                {
                    ID = 1,
                    Name = "some random item",
                    Type = new Dictionary<string, ItemType>()
                    {
                        {"Armor", new Armor()},
                        {"Consumable", new Consumable()}
                    }
                }
            }

        };
        strintitemdic.Add("eremon pls", qq);
        
    }
}
