using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystemTest
{
    public interface ISavable
    {
        public object data { get; }
        void Load(object data);
    }
}

