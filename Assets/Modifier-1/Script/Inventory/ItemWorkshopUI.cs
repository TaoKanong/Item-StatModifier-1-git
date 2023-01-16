using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorkshopUI
{
    // protected GameObject m_Position;
    protected Dictionary<string, GameObject> m_PositionUI = new Dictionary<string, GameObject>();
    protected InventoryDatabase m_PlayerDatabase;
    public InventoryDatabase playerDatabase => m_PlayerDatabase;
    protected PlayerShipConfig m_PlayerShipConfig;
    public PlayerShipConfig playerShipConfig => m_PlayerShipConfig;
    protected GameObject m_ModulePrefab;
    private List<GameObject> m_CurrInventoryItem = new List<GameObject>();
    public List<GameObject> currInventoryItem => m_CurrInventoryItem;
    private List<GameObject> m_CurrEquipmentItem = new List<GameObject>();
    public List<GameObject> currEquipmentItem => m_CurrEquipmentItem;
    private List<GameObject> m_Position = new List<GameObject>();
    // public List<GameObject> position => m_Position;

    public ItemWorkshopUI(InventoryDatabase playerDatabase, PlayerShipConfig playerShipConfig, List<GameObject> position)
    {
        m_PlayerDatabase = playerDatabase;
        m_PlayerShipConfig = playerShipConfig;

        foreach (GameObject pos in position)
        {
            m_PositionUI.Add(pos.name, pos);
        }
    }

    public void GenerateInventoryItemUI<T>(GameObject prefabGameObject, int idx) // main way
    {
        if (typeof(T) == typeof(ModuleInventoryDefinition))
        {
            int val = 0;
            foreach (ModuleInventoryDefinition mod in playerDatabase.playerModuleInventory)
            {
                ModuleModController moduleModController = prefabGameObject.GetComponent<ModuleModController>();
                moduleModController.mod = mod.mod;
                moduleModController.id = mod.id;
                moduleModController.itemBehaviour = ItemBehaviour.Equip;

                prefabGameObject.transform.SetParent(m_PositionUI["InventoryUI"].transform, m_PositionUI["InventoryUI"].transform.parent);
                ResizeToStandard(prefabGameObject);

                val++;
                if (idx == val) // ถ้าไม่ break loop มันจะ loop จนถึงรอบสุดท้ายตลอด ทำให้ object ที่ถูกใส่เข้ามากลายเป็นตัวสุดท้ายตลอด
                {
                    m_CurrInventoryItem.Add(prefabGameObject);
                    break; //
                }
            }
        }

        else if (typeof(T) == typeof(WeaponInventoryDefinition))
        {
            int val = 0;
            foreach (ModuleInventoryDefinition mod in playerDatabase.playerWeaponInventroy)
            {
                ModuleModController moduleModController = prefabGameObject.GetComponent<ModuleModController>();
                moduleModController.mod = mod.mod;
                moduleModController.id = mod.id;
                moduleModController.itemBehaviour = ItemBehaviour.Equip;

                prefabGameObject.transform.SetParent(m_PositionUI["InventoryUI"].transform, m_PositionUI["InventoryUI"].transform.parent);
                ResizeToStandard(prefabGameObject);

                val++;
                if (idx == val) // ถ้าไม่ break loop มันจะ loop จนถึงรอบสุดท้ายตลอด ทำให้ object ที่ถูกใส่เข้ามากลายเป็นตัวสุดท้ายตลอด
                {
                    m_CurrInventoryItem.Add(prefabGameObject);
                    break; //
                }
            }
        }

    }

    public void GenerateInventoryItemUI<T>(List<GameObject> prefabGameObject) // another way Test
    {
        if (typeof(T) == typeof(ModuleInventoryDefinition))
        {
            int val = 0;
            int idx = 1;
            while (idx == val)
            {
                foreach (ModuleInventoryDefinition mod in playerDatabase.playerModuleInventory)
                {
                    foreach (GameObject obj in prefabGameObject)
                    {
                        ModuleModController moduleModController = obj.GetComponent<ModuleModController>();
                        moduleModController.mod = mod.mod;
                        moduleModController.id = mod.id;
                        moduleModController.itemBehaviour = ItemBehaviour.Equip;

                        obj.transform.SetParent(m_PositionUI["InventoryUI"].transform, m_PositionUI["InventoryUI"].transform.parent);
                        ResizeToStandard(obj);

                        val++;
                        m_CurrInventoryItem.Add(obj);
                    }
                }
            }

        }
    }

    public void GenerateEquipmentItemUI<T>(GameObject prefabGameObject, int idx)
    {
        if (typeof(T) == typeof(ModuleInventoryDefinition))
        {
            int val = 0;
            foreach (ModuleInventoryDefinition mod in playerShipConfig.moduleModList) // 
            {
                ModuleModController moduleModController = prefabGameObject.GetComponent<ModuleModController>();
                moduleModController.mod = mod.mod;
                moduleModController.id = mod.id;
                moduleModController.itemBehaviour = ItemBehaviour.Remove;

                prefabGameObject.transform.SetParent(m_PositionUI["ModuleEquipmentUI"].transform, m_PositionUI["ModuleEquipmentUI"].transform.parent);
                ResizeToStandard(prefabGameObject);

                val++;
                if (idx == val) // ถ้าไม่ break loop มันจะ loop จนถึงรอบสุดท้ายตลอด ทำให้ object ที่ถูกใส่เข้ามากลายเป็นตัวสุดท้ายตลอด
                {
                    m_CurrEquipmentItem.Add(prefabGameObject);
                    break; //
                }
            }
        }
    }

    void ResizeToStandard(GameObject obj)
    {
        obj.transform.localScale = new Vector3(1, 1, 1);
    }
}
