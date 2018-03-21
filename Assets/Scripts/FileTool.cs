using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public static class FileTool  {

    public static T LoadObjectFromJson<T>(string filePath)
    {
        var debugText = GameObject.FindGameObjectWithTag("DebugText").GetComponent<Text>();

        Debug.Log("Loading file at path : " + Application.persistentDataPath + filePath);
        if (!File.Exists(filePath))
        {
            if (!Application.isEditor)
            {
                // if it doesn't ->          
                // open StreamingAssets directory and load the db ->
                CreateFile(filePath);
                WWW loadFile = new WWW("jar:file://" + Application.persistentDataPath + "!/assets/" + filePath);  // this is the path to your StreamingAssets in android

                while (!loadFile.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check

                // then save to Application.persistentDataPath
                filePath = Application.persistentDataPath + filePath;
                File.WriteAllBytes(filePath, loadFile.bytes);
            }
            else
            {
                filePath = Application.streamingAssetsPath + filePath;
            }
            //return default(T);
        }
        debugText.text = filePath;

        return JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath),
                                                            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
    }

    public static void SaveFileAsJson(string filePath, object obj)
    {

        if (!File.Exists(filePath))
        {
            CreateFile(filePath);
            
            Debug.Log(filePath);
            // TODO: If file doesn't exist, create one?.
            //return;
        }
        //Could not find a part of the path "C:\Users\nSr\AppData\LocalLow\DefaultCompany\ItemC:\Unity Projects\Item\Assets\StreamingAssets\Database\Storage.json".
        Debug.Log(Application.persistentDataPath + filePath);
        if (!Application.isEditor)
            filePath = Application.persistentDataPath + filePath;
        else
            filePath = Application.streamingAssetsPath + filePath;



        File.WriteAllText(filePath, JsonConvert.SerializeObject(obj,
                                                        Formatting.Indented,
                                                        new JsonSerializerSettings
                                                        {
                                                            TypeNameHandling = TypeNameHandling.All,
                                                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                        }));
    }

    static void CreateFile(string filePath)
    {
        var test = filePath.Split('/');
        string dir = Application.streamingAssetsPath + "/";
        for (int i = 0; i < test.Length-1; i++)
        {
            dir += test[i];
        }
        Directory.CreateDirectory(dir);

        if (!Application.isEditor)
        {
            filePath = Application.persistentDataPath + filePath;
        }
        else
        {
            filePath = Application.streamingAssetsPath + filePath;
        }

        Debug.Log(filePath);

        var fs = File.Create(filePath);
        fs.Close();
        

    }
    
}
