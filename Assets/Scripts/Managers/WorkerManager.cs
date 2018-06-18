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
        var temp = FileTool.LoadObjectFromJson<List<Worker>>(PATH);
        if (temp == null)
            return;

        Instance.Workers.AddRange(temp);
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
        //Debug.Log("wc " +  Workers.Count);
        //foreach (var worker in Workers)
        //{
        //    Debug.Log("Looped " + Workers.Count);
        //    if (worker.OriginNode == cityName)
        //    {
        //        cityWorkers.Add(worker);
        //    }
        //}
        //Debug.Log(cityWorkers.Count);
        //return cityWorkers;
        return Workers.Where(x => x.OriginNode == cityName).ToList();
        //return Workers.Select(o => o.OriginNode.gameObject.name == cityName).ToList();
    }

    public void AddWorker(Worker worker)
    {
        Workers.Add(worker);
    }




}
