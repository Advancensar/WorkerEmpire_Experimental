using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public class SubNode : MonoBehaviour
{
    public Node MainNode;
    public List<Item> Resources = new List<Item>();
    public bool Unlocked;

    private void Awake()
    {
        MainNode = transform.parent.GetComponent<Node>();
    }

    public int GetWorkTime()
    {
        return Mathf.Max(Resources.Select(x => ((Resource)x.GetTypeByName("Resource")).WorkTime).ToArray());
    }
}
