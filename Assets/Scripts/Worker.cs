using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Worker
{
    public int ID;

    public string OriginNode = "";
    public string WorkNode = "";
    public DateTime StartTime;
    public int WorkTime = 1;

    public int WorkerLevel = 0;

    public int Lumber = 1;
    public int Metal = 1;
    public int Ore = 1;
    public int Speed = 1;
    public int Workspeed = 1;

    // Worktime = travel + work + travel
    // travel = distance / speed;
    
    public void SetupWork(SubNode subNode)
    {
        Debug.Log("asdasd" + subNode.MainNode().gameObject.name);
        WorkNode = subNode.MainNode().gameObject.name + "/" + subNode.name;
        StartTime = DateTime.Now;
        //WorkTime = ((MainNode) NodeManager.Instance.Nodes[WorkNode.Split('/')[0]]).SubNodes[WorkNode.Split('/')[1]].GetWorkTime();
        WorkTime = 5;
    }

    public void Work()
    {
        Debug.Log("umarım milyon kez dönmez");
        var elapsedSeconds = (DateTime.Now - StartTime).TotalSeconds;
        if (elapsedSeconds < 1)
            return;

        for (int i = 0; i < (int)elapsedSeconds/WorkTime; i++)
        {
            //items.Add(((MainNode) NodeManager.Instance.Nodes[WorkNode.Split('/')[0]]).SubNodes[WorkNode.Split('/')[1]].Resources);
            //foreach (var item in ((MainNode)NodeManager.Instance.Nodes[WorkNode.Split('/')[0]]).SubNodes[WorkNode.Split('/')[1]].Resources)
            //{
            //    InventoryManager.Instance.GetInventoryByName(OriginNode).AddItem(item);
            //}
        }

        StartTime = DateTime.Now.AddSeconds(-(int)(elapsedSeconds % WorkTime));

    }

    public void cop()
    {
        Debug.Log((DateTime.Now - StartTime).TotalSeconds);
    }

    struct IIStruct
    {
        public int Min;
        public int Max;
    }
    public Worker GenerateWorker(int workerLevel, string cityName)
    {
        var worker = new Worker {WorkerLevel = workerLevel, OriginNode = cityName};

        IIStruct highRollStat = new IIStruct() {Min = 4 + workerLevel, Max = 7 + workerLevel};
        IIStruct lowRollStat = new IIStruct() {Min = 1 + workerLevel, Max = 3 + workerLevel};
        IIStruct speedStat = new IIStruct() { Min = 1 + workerLevel, Max = 7 + workerLevel };

        switch (Random.Range(0, 2))
        {
            case 0:
                worker.Lumber = Random.Range(highRollStat.Min, highRollStat.Max);
                worker.Metal = Random.Range(lowRollStat.Min, lowRollStat.Max);
                worker.Ore = Random.Range(lowRollStat.Min, lowRollStat.Max);
                break;
            case 1:
                worker.Lumber = Random.Range(lowRollStat.Min, lowRollStat.Max);
                worker.Metal = Random.Range(highRollStat.Min, highRollStat.Max);
                worker.Ore = Random.Range(lowRollStat.Min, lowRollStat.Max);
                break;
            case 2:
                worker.Lumber = Random.Range(lowRollStat.Min, lowRollStat.Max);
                worker.Metal = Random.Range(lowRollStat.Min, lowRollStat.Max);
                worker.Ore = Random.Range(highRollStat.Min, highRollStat.Max);
                break;
        }

        worker.Speed = Random.Range(speedStat.Min, speedStat.Max);
        worker.Workspeed = Random.Range(speedStat.Min, speedStat.Max);


        return worker;
    }

    public string GetInfo()
    {
        return $"Lumber {Lumber}" + $"Metal {Metal}" + $"Ore {Ore}" + $"Speed {Speed}" + $"Workspeed {Workspeed}";

    }

    public Sprite GetAvatar()
    {
        return Resources.Load<Sprite>("Sprites/WorkerAvatar/Level_" + WorkerLevel);
    }


}
