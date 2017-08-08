using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UnityEngine;

public static class ReadAndWriteMessage
{
    public static void WriteMessage(string stringJson)
    {
        if (!File.Exists(GetPath()))
        {
            FileStream fs = new FileStream(GetPath(), FileMode.CreateNew);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(stringJson);
            sw.Flush();
            sw.Close();
        }
    }

    public static string ReadMessage()
    {
        if (File.Exists(GetPath()))
        {
            FileStream fs = new FileStream(GetPath(),FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string stringJson=sr.ReadToEnd();
            sr.Close();
            return stringJson;
        }
        return null;
    }

    private static string GetPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/StreamingAssets/Message.txt";

#elif UNITY_STANDALONE_WIN
        return Application.dataPath + "/StreamingAssets/Message.txt";
#elif UNITY_ANDROID
        return  Application.persistentDataPath + "/Message.txt";
#endif
    }
}

