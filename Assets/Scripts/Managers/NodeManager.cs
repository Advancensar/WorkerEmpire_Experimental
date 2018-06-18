using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NodeManager  
{
    public static NodeManager Instance { get; } = new NodeManager();

    public Dictionary<string, Node> Nodes = new Dictionary<string, Node>();

    private const string PATH = @"/Database/NodeSaveData.json";

    public void SaveNodeData()
    {
        var tempNodeData = new Dictionary<string, bool>();


        foreach (var nodeName in Nodes.Keys.ToList())
        {
            if (Nodes[nodeName].GetType() == typeof(MainNode))
            {
                foreach (var subNodeName in ((MainNode)Nodes[nodeName]).SubNodes.Keys.ToList())
                {
                    if (((MainNode) Nodes[nodeName]).SubNodes[subNodeName].Unlocked)
                    {
                        tempNodeData.Add(nodeName+"/"+subNodeName, true);
                    }
                }
            }

            if (Nodes[nodeName].Unlocked)
            {
                tempNodeData.Add(nodeName, true);
            }

        }
        FileTool.SaveFileAsJson(PATH, tempNodeData);
    }

    public void LoadNodeData()
    {
        var tempNodeData = FileTool.LoadObjectFromJson<Dictionary<string, bool>>(PATH);

        if (tempNodeData == null)
            return;

        foreach (var nodeName in tempNodeData.Keys.ToList())
        {
            //Debug.Log(address.Split('/')[0]);
            if (Instance.Nodes.ContainsKey(nodeName))
            {
                Instance.Nodes[nodeName].Unlocked = tempNodeData[nodeName];
                //Debug.Log(address);
            }
            else
            {
                ((MainNode) Instance.Nodes[nodeName.Split('/')[0]]).SubNodes[nodeName.Split('/')[1]].Unlocked =
                    tempNodeData[nodeName];
            }
        }
    }


}
