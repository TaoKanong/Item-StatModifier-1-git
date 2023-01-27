using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData : MonoBehaviour
{



    // private static SaveData _Instance;
    // public static SaveData Instance
    // {
    //     get
    //     {
    //         if (_Instance == null)
    //         {
    //             _Instance = new SaveData();
    //             return _Instance;
    //         }
    //         return _Instance;
    //     }
    //     set
    //     {
    //         _Instance = value;
    //     }
    // }
    public static SaveData instance;
    public InventoryDatabase playerDatabase;
    public PlayerShipConfig playerShipConfig;
    [HideInInspector] public string json;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        //Load
        Load<InventoryDatabase>("playerDatabase", playerDatabase);
        Load<PlayerShipConfig>("playerShipConfig", playerShipConfig);
    }

    private void OnApplicationQuit()
    {
        // //Save
        // var obj = ScriptableObject.CreateInstance<InventoryDatabase>();
        // obj = playerDatabase;
        // var json = JsonUtility.ToJson(obj);
        // Debug.Log(json);

        // SerializationManager.SaveDataToBinary("playerDatabase", json); // item scriptable object

        Save<InventoryDatabase>("playerDatabase", playerDatabase);
        Save<PlayerShipConfig>("playerShipConfig", playerShipConfig);

        // var database = ScriptableObject.CreateInstance<InventoryDatabase>();
        // var module = ScriptableObject.CreateInstance<ModuleMod>();

        // database = playerDatabase;
        // var json = JsonUtility.ToJson(playerDatabase);
        // Debug.Log(json);

        // SerializationManager.SaveDataToBinary("playerDatabase", json); // item scriptable object
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Save<InventoryDatabase>("playerDatabase", playerDatabase);
            Save<PlayerShipConfig>("playerShipConfig", playerShipConfig);

            // var database = ScriptableObject.CreateInstance<InventoryDatabase>();
            // var module = ScriptableObject.CreateInstance<ModuleMod>();

            // database = playerDatabase;
            // var json = JsonUtility.ToJson(playerDatabase);
            // Debug.Log(json);

            // SerializationManager.SaveDataToBinary("playerDatabase", json); // item scriptable object
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Load<InventoryDatabase>("playerDatabase", playerDatabase);
            Load<PlayerShipConfig>("playerShipConfig", playerShipConfig);
        }
    }

    void Save<T>(string saveName, T data) where T : ScriptableObject
    {
        if (Application.isEditor)
        {
            //Save
            var obj = ScriptableObject.CreateInstance<T>();
            obj = data;
            var json = JsonUtility.ToJson(obj);
            Debug.Log(json);

            SerializationManager.SaveDataToBinary(saveName + "editor", json); // item scriptable object
        }

        else
        {
            //Save
            var obj = ScriptableObject.CreateInstance<T>();
            obj = data;
            var json = JsonUtility.ToJson(obj);
            Debug.Log(json);

            SerializationManager.SaveDataToBinary(saveName, json); // item scriptable object
        }

    }

    void Load<T>(string saveName, T data) where T : ScriptableObject
    {
        if (Application.isEditor)
        {
            json = (string)SerializationManager.LoadDataFromBinary(Application.persistentDataPath + "/saves/" + saveName + "editor" + ".save");

            JsonUtility.FromJsonOverwrite(json, data);
        }

        else
        {
            json = (string)SerializationManager.LoadDataFromBinary(Application.persistentDataPath + "/saves/" + saveName + ".save");

            JsonUtility.FromJsonOverwrite(json, data);
        }
    }
}