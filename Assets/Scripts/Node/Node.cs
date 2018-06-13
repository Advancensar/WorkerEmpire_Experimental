using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public bool Unlocked;
    public List<Node> ConnectedNodes = new List<Node>();

    protected virtual void Awake()
    {
        NodeManager.Instance.Nodes.Add(gameObject.name, this);
    }
}
