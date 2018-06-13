using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NodeWindow : MonoBehaviour
{
    public GameObject RequiredItemPrefab;
    public GameObject UnlockButton;
    public GameObject ManageButton;
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI NodeName;

    public Node Node;

    public void LoadWindowInfo(Node node)
    {
        gameObject.SetActive(true);

        Node = node;
        NodeName.text = node.gameObject.name;
        Cost.text = node.UnlockCost.ToString();
        if (node.Unlocked)
        {
            UnlockButton.SetActive(false);
            if (node.GetType() == typeof(SubNode))
            {
                ManageButton.SetActive(true);
            }
        }
        else if (!(node.GetType() == typeof(SubNode) && node.Unlocked))
        {
            ManageButton.SetActive(false);
        }
        
    }

	void Start() 
	{
        //LoadWindowInfo(GameManager.Instance.sub);
	}

    public void Unlock()
    {
        Debug.Log("Unlocking node" + PlayerManager.Instance.Gold);
        
        if (Node.UnlockCost > PlayerManager.Instance.Gold)
            return;

        Debug.Log("Unlocked node");
        Node.Unlocked = true;
        PlayerManager.Instance.Gold -= Node.UnlockCost;
        SaveManager.SaveNodes();
        LoadWindowInfo(Node);

        //LoadWindowInfo();

    }

    public void Manage()
    {
        GameManager.Instance.WorkerWindow.gameObject.SetActive(true);
        GameManager.Instance.WorkerWindow.LoadWindowInfo((SubNode)Node);

    }
}
