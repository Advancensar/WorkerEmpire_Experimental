using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public class SubNode : Node
{
    public List<Item> Resources = new List<Item>();

    protected override void Awake()
    {
        //ConnectedNodes.Add(transform.parent.GetComponent<MainNode>());
        //MainNode().SubNodes.Add(gameObject.name, this);
        //MainNode().ConnectedNodes.Add(this);
        base.Awake();
    }

    public int GetWorkTime()
    {
        var asd = Mathf.Max(Resources.Select(x => ((Resource)x.GetTypeByName("Resource")).WorkTime).ToArray());
        Debug.Log(asd);
        return 5;
    }

    public MainNode MainNode()
    {
        return ((MainNode) ConnectedNodes[0]);
    }

}
