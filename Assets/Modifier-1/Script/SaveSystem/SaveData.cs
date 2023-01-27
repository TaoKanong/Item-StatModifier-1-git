using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystemTest
{
    [CreateAssetMenu(menuName = "SaveSystem/SaveData", fileName = "SaveData", order = 0)]
    public class SaveData : ScriptableObject
    {
        [SerializeField] private LoadDataChannel m_LoadDataChannel;
        [SerializeField] private SaveDataChannel m_SaveDataChannel;
        [SerializeField] private string fileName;
        [HideInInspector, SerializeField] private string m_Path;
        private Dictionary<string, object> m_Data = new Dictionary<string, object>();
        public bool previousFileExist => File.Exists(m_Path);

        public void DeleteSave()
        {
            if (previousFileExist)
            {
                File.Delete(m_Path);
            }
        }

        public void Save(string id, object data)
        {
            m_Data[id] = data;
        }

        public void Load(string id, out object data)
        {
            data = m_Data[id];
        }

        public void Load()
        {
            FileManager.LoadFromBinaryFile(m_Path, out m_Data);
            m_LoadDataChannel.Load();
            m_Data.Clear();
        }

        public void Save()
        {
            if (previousFileExist)
            {
                FileManager.LoadFromBinaryFile(m_Path, out m_Data);
            }
            m_SaveDataChannel.Save();
            FileManager.SaveToBinaryFile(m_Path, m_Data);
            m_Data.Clear();
        }

        private void OnValidate()
        {
            m_Path = Path.Combine(Application.persistentDataPath + fileName);
        }
    }
}

