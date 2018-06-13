using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubnodeButton : MonoBehaviour 
{
    private void OnMouseDown()
    {
        GameManager.Instance.NodeWindow.LoadWindowInfo(transform.parent.GetComponent<SubNode>());
    }

}
