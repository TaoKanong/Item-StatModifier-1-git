using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickUpMode
{
    Module,
    Weapon
}

public class ModulePickup : MonoBehaviour
{
    [SerializeField] private ModuleMod[] m_randMod;
    [SerializeField] private Weapon[] m_randWeapon;
    public PickUpMode currPickUpMode;
    void Start()
    {
        // currPickUpMode = PickUpMode.Module;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnHandlePickUp()
    {
        RandomModule();
    }

    void RandomModule()
    {
        if (currPickUpMode == PickUpMode.Module)
        {
            ModuleMod currMod;
            int rand = Random.Range(0, m_randMod.Length);
            currMod = m_randMod[rand];

            InventorySystem.Instance.AddItem(currMod);
        }
        else
        {
            Weapon currWeapon;
            int rand = Random.Range(0, m_randMod.Length);
            currWeapon = m_randWeapon[rand];

            InventorySystem.Instance.AddItem(currWeapon);
        }
    }

    public void ClearItem()
    {
        InventorySystem.Instance.ClearItem();
    }
}
