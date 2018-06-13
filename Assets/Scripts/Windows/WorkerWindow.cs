using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorkerWindow : MonoBehaviour
{
    public Transform Content;
    public GameObject WorkerPrefab;
    public SubNode Node;

    private void Start()
    {
    }

    public void LoadWindowInfo(SubNode node)
    {
        LoadWindowInfo(true);
        Node = node;
    }

    public void LoadWindowInfo(bool WorkWindow = false)
    {
        ClearContent();

        foreach (var worker in WorkerManager.Instance.Workers)
        {
            var workerObject = Instantiate(WorkerPrefab, Content, worldPositionStays: false);
            //workerObject.transform.Find("Text").GetComponent<TextMeshProUGUI>().text =
            //    worker.OriginNode + " : " + worker.WorkerLevel + " : " + worker.WorkNode;
            LoadWorkerObject(workerObject.transform.Find("Values"), worker);
            workerObject.transform.Find("WorkButton").gameObject.SetActive(WorkWindow);
            workerObject.transform.Find("WorkButton").GetComponent<Button>().onClick.AddListener(WorkButton);
            workerObject.GetComponent<WorkerObject>().Worker = worker;


        }

    }

    public void WorkButton()
    {
        EventSystem.current.currentSelectedGameObject.transform.parent.GetComponent<WorkerObject>().Worker
            .SetupWork(Node);
    }

    public void ClearContent()
    {
        foreach (Transform child in Content)
        {
            Destroy(child.gameObject);
        }
    }


    public void LoadWorkerObject(Transform values, Worker worker)
    {

        values.parent.transform.Find("Image").GetComponent<Image>().sprite = worker.GetAvatar();
        values.parent.transform.Find("OriginNode_Text").GetComponent<TextMeshProUGUI>().text = worker.OriginNode;
        values.parent.transform.Find("WorkNode_Text").GetComponent<TextMeshProUGUI>().text = worker.WorkNode;

        values.Find("Lumber").GetComponent<TextMeshProUGUI>().text = worker.Lumber.ToString();
        values.Find("Metal").GetComponent<TextMeshProUGUI>().text = worker.Metal.ToString();
        values.Find("Ore").GetComponent<TextMeshProUGUI>().text = worker.Ore.ToString();
        values.Find("Speed").GetComponent<TextMeshProUGUI>().text = worker.Speed.ToString();
        values.Find("Workspeed").GetComponent<TextMeshProUGUI>().text = worker.Workspeed.ToString();
    }

}
