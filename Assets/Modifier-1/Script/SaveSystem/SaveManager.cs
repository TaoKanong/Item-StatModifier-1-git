using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystemTest
{
    [DefaultExecutionOrder(1)]
    public class SaveManager : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private SaveData m_SaveData;

        private void Awake()
        {
            if (m_SaveData.previousFileExist)
            {
                m_SaveData.Load();
            }
        }

        private void OnApplicationQuit()
        {
            m_SaveData.Save();
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

