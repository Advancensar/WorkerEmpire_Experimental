using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorkerContractWindow : MonoBehaviour
{
    public Image WorkerImage;
    public Transform Values;
    public int WorkerGoldCost = 500;
    public int WorkerEnergyCost = 200;

    private Worker RolledWorker;
    private string WorkerCity;


    private void Awake()
    {
        //RollWorker("Heidel");
        LoadWindowInfo("Heidel");
    }

    public void LoadWindowInfo(string CityName)
    {
        WorkerCity = CityName;
        RollWorker();
    }

    public void RollWorker()
    {
        if (PlayerManager.Instance.Energy < WorkerEnergyCost)
            return;

        RolledWorker = WorkerManager.Instance.RandomWorker(WorkerCity);
        WorkerImage.sprite = RolledWorker.GetAvatar();
        FillValues();
        PlayerManager.Instance.Energy -= WorkerEnergyCost;
        
    }

    public void FillValues()
    {
        Values.Find("Lumber").GetComponent<TextMeshProUGUI>().text = RolledWorker.Lumber.ToString();
        Values.Find("Metal").GetComponent<TextMeshProUGUI>().text = RolledWorker.Metal.ToString();
        Values.Find("Ore").GetComponent<TextMeshProUGUI>().text = RolledWorker.Ore.ToString();
        Values.Find("Speed").GetComponent<TextMeshProUGUI>().text = RolledWorker.Speed.ToString();
        Values.Find("Workspeed").GetComponent<TextMeshProUGUI>().text = RolledWorker.Workspeed.ToString();
    }


    public void HireWorker()
    {
        if (PlayerManager.Instance.Gold < (RolledWorker.WorkerLevel+1)* WorkerGoldCost)
            return;
        WorkerManager.Instance.AddWorker(RolledWorker);
        PlayerManager.Instance.Gold -= (RolledWorker.WorkerLevel + 1)*WorkerGoldCost;
        SaveManager.SaveWorkers();
    }

    //Text atamaları vs düzgün yapılacak @DONE
    //Node açma - kapama ve node'a worker atama sistemi
    // populate the game
    // tezi yaz


}
