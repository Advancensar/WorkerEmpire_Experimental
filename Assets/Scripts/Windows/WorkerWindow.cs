using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkerWindow : MonoBehaviour
{
    public Transform Content;
    public GameObject WorkerPrefab;

    public void LoadWindow(string cityName)
    {
        ClearContent();
        foreach (var worker in WorkerManager.Instance.GetWorkersOfCity(cityName))
        {
            var workerObject = Instantiate(WorkerPrefab, Content, worldPositionStays: false);
            workerObject.transform.Find("Text").GetComponent<TextMeshProUGUI>().text =
                worker.OriginNode + " : " + worker.WorkerLevel + " : " + worker.WorkNode;

            workerObject.transform.Find("Image").GetComponent<Image>().sprite = worker.GetAvatar();
        }
        
    }

    public void ClearContent()
    {
        foreach (Transform child in Content)
        {
            Destroy(child.gameObject);
        }
    }

}
