using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkerManager
{
    public static WorkerManager Instance { get; } = new WorkerManager();

    public List<Worker> Workers = new List<Worker>();

    private const string PATH = @"/Database/WorkerData.json";

    public void SaveWorkerData()
    {
        FileTool.SaveFileAsJson(PATH, Workers);
    }

    public void LoadWorkerData()
    {
        Workers = FileTool.LoadObjectFromJson<List<Worker>>(PATH);
    }

    public Worker RandomWorker(string cityName)
    {
        var worker = new Worker();

        var range = Random.Range(0, 100);

        if (range < 70)
        {
            worker = worker.GenerateWorker(0, cityName);
        }
        else if (range >= 70 && range < 90)
        {
            worker = worker.GenerateWorker(1, cityName);
        }
        else if (range >= 90)
        {
            worker = worker.GenerateWorker(2, cityName);
        }

        return worker;
    }


    public List<Worker> GetWorkersOfCity(string cityName)
    {
        //var cityWorkers = new List<Worker>();
        //foreach (var worker in Workers)
        //{
        //    if (worker.OriginNode.gameObject.name == cityName)
        //    {
        //        cityWorkers.Add(worker);
        //    }
        //}

        return Workers.Where(x => x.OriginNode == cityName).ToList();
        //return Workers.Select(o => o.OriginNode.gameObject.name == cityName).ToList();
    }

    public void AddWorker(Worker worker)
    {
        if (Workers == null)
        {
            Workers = new List<Worker>();
        }
        Workers.Add(worker);
    }

}
