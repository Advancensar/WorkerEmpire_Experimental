using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public HouseWindow HouseWindow;
    public InventoryWindow InventoryWindow;
    public CraftWindow CraftWindow;
    public WorkerWindow WorkerWindow;
    public WorkerContractWindow WorkerContractWindow;
    public NodeWindow NodeWindow;
    public ItemTooltipWindow ItemTooltipWindow;

    public MobileCamera MainCam;

    /// <summary>
    /// TEMP FIELDS
    /// </summary>
    public SubNode sub;
    /// <summary>
    /// 
    /// </summary>
    
    private void Start()
    {
        //string path = Application.streamingAssetsPath + @"/Database/Item_Database.json";
        //FileTool.TEST(path);
        //Storage.Instance.LoadInventory();
        CityManager.Instance.LoadPlayerHouseData();
        NodeManager.Instance.LoadNodeData();
        WorkerManager.Instance.LoadWorkerData();
        PlayerManager.Instance.LoadPlayerData();

        StartCoroutine(CheckWork());

    }

    private void Update()
    {
        //foreach (var worker in WorkerManager.Instance.Workers)
        //{
        //    Debug.Log(DateTime.Now.AddSeconds(-(int)((DateTime.Now - worker.StartTime).TotalSeconds % worker.WorkTime)));

        //}
        //foreach (var worker in WorkerManager.Instance.Workers)
        //{

        //    Debug.Log("umarım çökmez");
        //    worker.Work();
        //}

    }

    IEnumerator CheckWork()
    {
        while (true)
        {
            foreach (var worker in WorkerManager.Instance.Workers)
            {

                worker.Work();
            }
            PlayerManager.Instance.Energy++;
            yield return new WaitForSeconds(10.0f);
        }

    }

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        ItemDatabase.Instance.LoadDB();

        //Storage.Instance.InstantiateSlots();
    }
    
    public void save()
    {
        SaveManager.SaveEverything();
        //Storage.Instance.SaveInventory();
    }

}
