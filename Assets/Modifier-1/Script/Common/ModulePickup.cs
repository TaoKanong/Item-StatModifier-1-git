using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulePickup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ModuleMod m_Mod;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnHandlePickUp()
    {
        InventorySystem.Instance.AddItem(m_Mod);
    }
}
