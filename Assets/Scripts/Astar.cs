using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Astar
{

    public static GameObject goal;
    public static GameObject current;
    public static List<List<GameObject>> HesaplananYollar = new List<List<GameObject>>();
    public static List<GameObject> Result = new List<GameObject>();

    public static List<GameObject> FindPath(GameObject a, GameObject b)
    {
        goal = a;
        current = b;
        Result.Clear();
        HesaplananYollar.Clear();

        foreach (GameObject point in current.GetComponent<Node>().connectedNodesGO)
        {
            HesaplananYollar.Add(new List<GameObject>() { current, point });
        }

        while (current != goal)
        {
            NewPath();
        }

        return Result;
    }

    public static List<GameObject> Minimum()
    {
        var tempList = new List<GameObject>();


        if (!HesaplananYollar.Any())
        {
            tempList.Add(current);
            return tempList;
        }

        float temp = 0;
        float best = float.MaxValue;

        foreach (var item in HesaplananYollar)
        {
            for (int i = 0; i < item.Count - 1; i++)
            {
                temp += Distance(item[i].transform.position, item[i + 1].transform.position);
            }
            temp += Distance(item[item.Count - 1].transform.position, goal.transform.position);

            if (temp < best)
            {
                best = temp;
                tempList = item;
            }

            temp = 0;
        }
        return tempList;
    }

    public static float Distance(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt(Mathf.Pow((a.x - b.x), 2) + Mathf.Pow((a.y - b.y), 2) + Mathf.Pow((a.z - b.z), 2));
    }

    public static void CalculateNewPaths(List<GameObject> path, GameObject point)
    {
        var tempList = path.ToList();
        tempList.Add(point);

        float cost = 0;

        for (int i = 0; i < tempList.Count - 1; i++)
        {
            cost += Distance(tempList[i].transform.position, tempList[i + 1].transform.position);
        }
        cost += Distance(point.transform.position, goal.transform.position);

        HesaplananYollar.Add(tempList);

    }

    public static void NewPath()
    {
        var tempPath = Minimum();

        var nodes = tempPath[tempPath.Count - 1].GetComponent<Node>().connectedNodesGO;

        foreach (GameObject point in nodes)
        {
            CalculateNewPaths(tempPath, point);
        }

        HesaplananYollar.Remove(tempPath);

        Result = Minimum();
        current = Result[Result.Count - 1];
    }

    public static void ShowPaths()
    {
        string path = "";
        foreach (var Path in HesaplananYollar)
        {
            foreach (var point in Path)
            {
                path += "-" + point.name;
            }

            path += " / ";
        }

        Debug.Log(path);
    }

    public static string ShowThisPath(List<GameObject> lgo)
    {
        string path = "List of gos : ";
        foreach (var go in lgo)
        {
            path += "-" + go.name;
        }

        return path;
    }

    public static void ShowConnectedNodes(GameObject go)
    {
        string str = go.name + " : ";
        foreach (var g in go.GetComponent<Node>().connectedNodesGO)
        {
            str += "-" + g.name;
        }

        Debug.Log(str);
    }

    public static float CalculateDistance(List<GameObject> path)
    {
        float temp = 0;

        for (int i = 0; i < path.Count-1; i++)
        {
            temp += Distance(path[i].transform.position, path[i + 1].transform.position);
        }


        return temp;
    }
}
