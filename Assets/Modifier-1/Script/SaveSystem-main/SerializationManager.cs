using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public class SerializationManager
{
    public static bool SaveDataToBinary(string saveName, object saveData)
    {
        BinaryFormatter formatter = GetBinaryFormatter();
        if (!File.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        FileStream file = File.Create(Application.persistentDataPath + "/saves/" + saveName + ".save");

        formatter.Serialize(file, saveData);
        file.Close();
        return true;
    }

    public static object LoadDataFromBinary(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }

        BinaryFormatter formatter = GetBinaryFormatter();
        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            object saveData = formatter.Deserialize(file);
            file.Close();

            return saveData;
        }
        catch (System.Exception)
        {
            Debug.LogWarning($"cannot find path at {path}");
            file.Close();

            return null;
        }
    }

    public static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        return formatter;
    }
}
