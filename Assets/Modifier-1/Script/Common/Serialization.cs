using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public class Serialization
{
    public static bool Save(string saveName, object saveData)
    {
        BinaryFormatter formatter = GetBinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        string path = Application.persistentDataPath + "saves" + saveName + ".save";
        FileStream file = File.Create(path);
        formatter.Serialize(file, saveData);

        file.Close();
        return true;
    }

    public static object Load(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }

        BinaryFormatter formatter = GetBinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();

            return save;

        }
        catch (System.Exception)
        {
            Debug.Log("Fail to load file");
            return null;
            throw;
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter format = new BinaryFormatter();
        return format;
    }
}
