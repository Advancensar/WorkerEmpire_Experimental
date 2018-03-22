using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public static class FileTool {


    public static T LoadObjectFromJson<T>(string filePath)
    {
        if (!Application.isEditor)
        {
            if (!File.Exists(Application.persistentDataPath + filePath))
            {
                CreateFile(filePath);
                WWW loadFile = new WWW("jar:file://" + Application.dataPath + "!/assets" + filePath);  //If file doesn't exists get the file out of apk

                while (!loadFile.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
                                              // then save to Application.persistentDataPath
                                              // TODO : Set timeout for file loading.

                File.WriteAllBytes(Application.persistentDataPath + filePath, loadFile.bytes); // Write the data from jar to file at path
            }
            filePath = Application.persistentDataPath + filePath;

        }
        else
        {
            filePath = Application.streamingAssetsPath + filePath;
        }
        

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
        string dir = "";
        for (int i = 0; i < test.Length-1; i++)
        {
            dir += test[i] + "/";
        }
        dir = dir.Remove(dir.Length - 1);


        if (!Application.isEditor)
        {
            dir = Application.persistentDataPath + dir;
            filePath = Application.persistentDataPath + filePath;
        }
        else
        {
            dir = Application.streamingAssetsPath + dir;
            filePath = Application.streamingAssetsPath + filePath;
        }
        Directory.CreateDirectory(dir);
        
        var fs = File.Create(filePath);
        fs.Close();        
    }

    static void UnpackStreamingAsset(string filePath)
    {

    }    

    
}
