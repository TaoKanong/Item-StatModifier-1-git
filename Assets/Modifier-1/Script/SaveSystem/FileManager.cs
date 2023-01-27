using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystemTest
{
    public class FileManager
    {
        public static void SaveToBinaryFile(string path, Dictionary<string, object> data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);

            try
            {
                formatter.Serialize(file, data);
            }
            catch (System.Exception)
            {

                Debug.LogWarning($"File to save file at {path}");
            }
            finally
            {
                file.Close();
            }
        }

        public static void LoadFromBinaryFile(string path, out Dictionary<string, object> data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);

            try
            {
                data = formatter.Deserialize(file) as Dictionary<string, object>;
            }
            catch (System.Exception)
            {
                Debug.LogWarning($"File to load file at {path}");
                data = new Dictionary<string, object>();
            }
            finally
            {
                file.Close();
            }
        }
    }
}

