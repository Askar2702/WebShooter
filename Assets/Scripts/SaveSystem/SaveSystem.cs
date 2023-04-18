using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveSystem
{
    [System.Obsolete]
    public static void SaveData(Game gameData)
    {
          BinaryFormatter formatter = new BinaryFormatter();
//#if UNITY_EDITOR
        string path = Application.persistentDataPath + "/GameDataShooter.txt";
//#elif UNITY_WEBGL
//        if (!Directory.Exists(savePathName))
//        {
//            Directory.CreateDirectory(savePathName);
//        }
//#endif
        FileStream stream = new FileStream(path, FileMode.Create);
        GameData Data = new GameData(gameData);
        formatter.Serialize(stream, Data);
        stream.Close();
        Application.ExternalEval("_JS_FileSystem_Sync();");
    }

    public static GameData LoadData()
    {
        string path = Application.persistentDataPath + "/GameDataShooter.txt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.Log("файла нету");
            return null;
        }
    }
}

