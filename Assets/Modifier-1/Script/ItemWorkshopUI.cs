using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Object;

public class ItemWorkshopUI
{
    // protected GameObject m_Position;
    protected Dictionary<string, GameObject> m_PositionUI = new Dictionary<string, GameObject>();
    protected InventoryDatabase m_PlayerDatabase;
    public InventoryDatabase playerDatabase => m_PlayerDatabase;
    protected PlayerShipConfig m_PlayerShipConfig;
    public PlayerShipConfig playerShipConfig => m_PlayerShipConfig;
    private List<GameObject> m_CurrInventoryItem = new List<GameObject>();
    public List<GameObject> currInventoryItem => m_CurrInventoryItem;
    private List<GameObject> m_CurrEquipmentModule = new List<GameObject>();
    public List<GameObject> currEquipmentModule => m_CurrEquipmentModule;
    private List<GameObject> m_CurrEquipmentWeapon = new List<GameObject>();
    public List<GameObject> currEquipmentWeapon => m_CurrEquipmentWeapon;

    public ItemWorkshopUI(InventoryDatabase playerDatabase, PlayerShipConfig playerShipConfig, List<GameObject> position)
    {
        m_PlayerDatabase = playerDatabase;
        m_PlayerShipConfig = playerShipConfig;

        foreach (GameObject pos in position)
        {
            m_PositionUI.Add(pos.name, pos);
        }
    }

    public void GenerateInventoryItemUI<T>(GameObject prefabGameObject, ref List<GameObject> list) // main way
    {
        RefreshItemUI(ref list);

        if (typeof(T) == typeof(ModuleInventoryDefinition))
        {
            foreach (ModuleInventoryDefinition mod in playerDatabase.playerModuleInventory)
            {
                GameObject newGameObject = UnityEngine.Object.Instantiate(prefabGameObject);
                ModuleModController moduleModController = newGameObject.GetComponent<ModuleModController>();
                moduleModController.mod = mod.mod;
                moduleModController.id = mod.id;
                moduleModController.itemBehaviour = ItemBehaviour.Equip;
                moduleModController.icon = mod.mod.icon;

                newGameObject.transform.SetParent(m_PositionUI["InventoryUI"].transform, m_PositionUI["InventoryUI"].transform.parent);
                ResizeToStandard(newGameObject);
                m_CurrInventoryItem.Add(newGameObject);
            }
            list = currInventoryItem;
        }

        else if (typeof(T) == typeof(WeaponInventoryDefinition))
        {
            foreach (WeaponInventoryDefinition weapon in playerDatabase.playerWeaponInventroy)
            {
                GameObject newGameObject = UnityEngine.Object.Instantiate(prefabGameObject);
                WeaponController weaponController = newGameObject.GetComponent<WeaponController>();
                weaponController.weapon = weapon.weapon;
                weaponController.id = weapon.id;
                weaponController.itemBehaviour = ItemBehaviour.Equip;
                weaponController.weaponType = weapon.weapon.weaponType;
                weaponController.icon = weapon.weapon.icon;

                newGameObject.transform.SetParent(m_PositionUI["InventoryUI"].transform, m_PositionUI["InventoryUI"].transform.parent);
                ResizeToStandard(newGameObject);
                m_CurrInventoryItem.Add(newGameObject);
            }
            list = currInventoryItem;
        }

    }

    public void GenerateEquipmentItemUI(GameObject modulePrefUI, GameObject weaponPrefUI, ref List<GameObject> moduleList, ref List<GameObject> weaponList)
    {
        RefreshItemUI(ref moduleList);
        RefreshItemUI(ref weaponList);

        foreach (ModuleInventoryDefinition mod in playerShipConfig.moduleModList) // 
        {
            GameObject newGameObject = UnityEngine.Object.Instantiate(modulePrefUI);
            ModuleModController moduleModController = newGameObject.GetComponent<ModuleModController>();
            moduleModController.mod = mod.mod;
            moduleModController.id = mod.id;
            moduleModController.itemBehaviour = ItemBehaviour.Remove;
            moduleModController.icon = mod.mod.icon;

            newGameObject.transform.SetParent(m_PositionUI["ModuleEquipmentUI"].transform, m_PositionUI["ModuleEquipmentUI"].transform.parent);
            ResizeToStandard(newGameObject);
            m_CurrEquipmentModule.Add(newGameObject);
        }




        // Instantiate Primary
        if (playerShipConfig.primaryWeapon)
        {
            GameObject primaryWeaponUI = UnityEngine.Object.Instantiate(weaponPrefUI);
            WeaponController primaryWeaponProperties = primaryWeaponUI.GetComponent<WeaponController>();
            primaryWeaponProperties.weapon = playerShipConfig.primaryWeapon;
            primaryWeaponProperties.id = 0;
            primaryWeaponProperties.weaponType = playerShipConfig.primaryWeapon.weaponType;
            primaryWeaponProperties.itemBehaviour = ItemBehaviour.Remove;
            primaryWeaponProperties.icon = playerShipConfig.primaryWeapon.icon;

            primaryWeaponUI.transform.SetParent(m_PositionUI["PrimaryEquipUI"].transform, m_PositionUI["PrimaryEquipUI"].transform.parent);
            ResizeToStandard(primaryWeaponUI);
            m_CurrEquipmentWeapon.Add(primaryWeaponUI);

        }

        if (playerShipConfig.secondaryWeapon)
        {
            // Instantiate Secondary
            GameObject secondaryWeaponUI = UnityEngine.Object.Instantiate(weaponPrefUI);
            WeaponController secondaryWeaponProperties = secondaryWeaponUI.GetComponent<WeaponController>();
            secondaryWeaponProperties.weapon = playerShipConfig.secondaryWeapon;
            secondaryWeaponProperties.id = 1;
            secondaryWeaponProperties.weaponType = playerShipConfig.secondaryWeapon.weaponType;
            secondaryWeaponProperties.itemBehaviour = ItemBehaviour.Remove;
            secondaryWeaponProperties.icon = playerShipConfig.secondaryWeapon.icon;

            secondaryWeaponUI.transform.SetParent(m_PositionUI["SecondaryEquipUI "].transform, m_PositionUI["SecondaryEquipUI "].transform.parent);
            ResizeToStandard(secondaryWeaponUI);
            m_CurrEquipmentWeapon.Add(secondaryWeaponUI);

        }


        moduleList = currEquipmentModule;
        weaponList = currEquipmentWeapon;
    }

    public void RefreshItemUI(ref List<GameObject> list)
    {
        if (list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                UnityEngine.Object.Destroy(list[i]);
            }
            list.Clear();
        }
    }

    void ResizeToStandard(GameObject obj)
    {
        obj.transform.localScale = new Vector3(1, 1, 1);
    }
}
