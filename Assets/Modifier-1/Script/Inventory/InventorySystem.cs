using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentInventory
{
    Module,
    Weapon,
    Ship,
}

public class InventorySystem : MonoBehaviour
{
    // Start is called before the first frame update
    public ModuleInventoryDatabase playerModuleDatabase;
    public ModuleInventoryDatabase playerWeaponDatabase;
    // public WeaponInventoryDatabase playerWeaponDatabase;
    [SerializeField] private GameObject m_ModulePrefab;
    [SerializeField] private GameObject m_InventoryUI;
    [SerializeField] private GameObject m_WeaponEquipment;
    [SerializeField] private GameObject m_ModuleEquipmentUI;
    private ModuleModController moduleModController;
    public List<GameObject> currItem = new List<GameObject>();
    public CurrentInventory currentInventory;
    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        currentInventory = CurrentInventory.Module;
        foreach (ModuleMod mod in playerModuleDatabase.playerModuleInventory)
        {
            GameObject newObj = Instantiate(m_ModulePrefab);
            moduleModController = newObj.GetComponent<ModuleModController>();
            moduleModController.mod = mod;
            moduleModController.itemBehaviour = ItemBehaviour.Equip;

            currItem.Add(newObj);
            newObj.transform.SetParent(m_InventoryUI.transform, m_InventoryUI.transform.parent);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectModule()
    {
        if (currentInventory == CurrentInventory.Weapon)
        {
            if (currItem.Count > 0)
            {
                for (int i = 0; i < currItem.Count; i++)
                {
                    Destroy(currItem[i]);
                }
                currItem.Clear();
            }

            foreach (ModuleMod mod in playerModuleDatabase.playerModuleInventory)
            {
                GameObject newObj = Instantiate(m_ModulePrefab);
                moduleModController = newObj.GetComponent<ModuleModController>();
                moduleModController.mod = mod;
                moduleModController.itemBehaviour = ItemBehaviour.Equip;

                currItem.Add(newObj);
                newObj.transform.SetParent(m_InventoryUI.transform, m_InventoryUI.transform.parent);
            }

            currentInventory = CurrentInventory.Module;
        }
    }

    public void SelectWeapon()
    {
        if (currentInventory == CurrentInventory.Module)
        {
            if (currItem.Count > 0)
            {
                for (int i = 0; i < currItem.Count; i++)
                {
                    Destroy(currItem[i]);
                }
                currItem.Clear();
            }

            foreach (ModuleMod mod in playerWeaponDatabase.playerModuleInventory)
            {
                GameObject newObj = Instantiate(m_ModulePrefab);
                moduleModController = newObj.GetComponent<ModuleModController>();
                moduleModController.mod = mod;
                moduleModController.itemBehaviour = ItemBehaviour.Equip;

                currItem.Add(newObj);
                newObj.transform.SetParent(m_InventoryUI.transform, m_InventoryUI.transform.parent);
            }

            currentInventory = CurrentInventory.Weapon;
        }
    }
}
