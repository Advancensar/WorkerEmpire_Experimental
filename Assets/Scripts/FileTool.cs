using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FileTool  {

    public static T LoadObjectFromJson<T>(string filePath)
    {
        Debug.Log("Loading file at path : " + filePath);
        if (!File.Exists(filePath))
        {
            return default(T);
        }

        return JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath),
                                                            new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
    }

    public static void SaveFileAsJson(string filePath, object obj)
    {
        if (!File.Exists(filePath))
            return;

        File.WriteAllText(filePath, JsonConvert.SerializeObject(obj,
                                                        Formatting.Indented,
                                                        new JsonSerializerSettings
                                                        {
                                                            TypeNameHandling = TypeNameHandling.All,
                                                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                        }));
    }
    
}
