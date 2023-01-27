using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystemTest
{
    [CreateAssetMenu(menuName = "SaveSystem/channel/LoadDataChannel", fileName = "LoadDataChannel", order = 0)]
    public class LoadDataChannel : ScriptableObject
    {
        public event Action load;

        public void Load()
        {
            load?.Invoke();
        }
    }
}

