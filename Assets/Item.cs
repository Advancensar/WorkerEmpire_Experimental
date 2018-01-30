using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour {
    [Header("-- Default Inspector --")]
    public int ID;
    public string Name;
    public ItemType Type = new ItemType();

    private void Start()
    {
        
    }

}
