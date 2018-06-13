using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainNode : Node 
{
    public Dictionary<string,SubNode> SubNodes = new Dictionary<string, SubNode>();

    protected override void Awake()
    {
        base.Awake();
        foreach (Transform child in transform)
        {
            SubNodes.Add(child.name,child.GetComponent<SubNode>());
        }
    }
    
}
