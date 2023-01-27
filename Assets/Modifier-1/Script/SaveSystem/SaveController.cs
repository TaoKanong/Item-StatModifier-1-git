using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystemTest
{
    public class SaveController : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private SaveData m_SaveData;
        [SerializeField] private SaveDataChannel m_SaveDataChannel;
        [SerializeField] private LoadDataChannel m_LoadDataChannel;
        [HideInInspector, SerializeField] private string m_Id;

        void Reset()
        {
            m_Id = Guid.NewGuid().ToString();
        }

        private void OnEnable()
        {
            m_LoadDataChannel.load += OnLoadData;
            m_SaveDataChannel.save += OnSaveData;
        }

        private void OnDisable()
        {
            m_LoadDataChannel.load -= OnLoadData;
            m_SaveDataChannel.save -= OnSaveData;
        }


        public void OnSaveData()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            foreach (ISavable savable in GetComponents<ISavable>())
            {
                data[savable.GetType().ToString()] = savable.data;
            }
            m_SaveData.Save(m_Id, data);
        }

        public void OnLoadData()
        {
            m_SaveData.Load(m_Id, out object data);
            Dictionary<string, object> dictionary = data as Dictionary<string, object>;
            foreach (ISavable savable in GetComponents<ISavable>())
            {
                savable.Load(dictionary[savable.GetType().ToString()]);
            }
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

