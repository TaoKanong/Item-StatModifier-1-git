using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystemTest
{
    [CreateAssetMenu(menuName = "SaveSystem/channel/SaveDataChannel", fileName = "SaveDataChannel", order = 0)]
    public class SaveDataChannel : ScriptableObject
    {
        public event Action save;
        public void Save()
        {
            save?.Invoke();
        }
    }
}

