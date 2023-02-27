using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum CurrentInventory
{
    Module,
    Weapon,
    Ship,
}

public class InventorySystem : Singleton<InventorySystem>
{
    // Start is called before the first frame update

    [Tooltip("Scriptable object database contain module")]
    [Header("Inventory Database")]
    [Space(10)]
    public InventoryDatabase playerDatabase;
    [Space(15)]

    [Header("PlayerConfig")]
    [Space(10)]
    public PlayerShipConfig playerShipConfig;
    // public WeaponInventoryDatabase playerWeaponDatabase;
    [Header("UI Prefab")]
    [Space(10)]
    [SerializeField] private GameObject m_ModulePrefab;
    [SerializeField] private GameObject m_WeaponPrefab;
    [Space(15)]
    [Header("UI Gameobject")]
    [Space(10)]
    // [SerializeField] private GameObject m_InventoryUI;
    // [SerializeField] private GameObject[] m_WeaponEquipment;
    // [SerializeField] private GameObject m_ModuleEquipmentUI;
    [SerializeField] private List<GameObject> m_Postion = new List<GameObject>();
    private ModuleModController moduleModController;
    // private ModuleModController WeaponController;
    public List<GameObject> currInventoryItem = new List<GameObject>();
    public List<GameObject> currEquipmentModule = new List<GameObject>();
    public List<GameObject> currEquipmentWeapon = new List<GameObject>();
    public CurrentInventory currentInventory; // item topic => module, weapon
    public event Action equipItem;
    public event Action unEquipItem; // for Update various ui
    public delegate void RemoveStatModule(ModuleMod moduleMod, int id); // ใช้กับ Statcontroller remove stat module
    public event RemoveStatModule unEquipModule;
    // public event Action equipWeapon;
    public delegate void RemoveStatWeapon(Weapon weapon, int id);
    public event RemoveStatWeapon unEquipWeapon;
    private ItemWorkshopUI itemWorkshopUI;
    private bool isInitialized;

    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        // var tests = new ItemWorkshopUI(playerDatabase, playerShipConfig, m_Postion);
        // tests.GenerateEquipmentItemUI
        itemWorkshopUI = new ItemWorkshopUI(playerDatabase, playerShipConfig, m_Postion);
        itemWorkshopUI.GenerateInventoryItemUI<ModuleInventoryDefinition>(m_ModulePrefab, ref currInventoryItem);
        itemWorkshopUI.GenerateEquipmentItemUI(m_ModulePrefab, m_WeaponPrefab, ref currEquipmentModule, ref currEquipmentWeapon);

        playerDatabase.ReArrangeItemID();
    }

    // Update is called once per frame
    void Update()
    {

    }

    ///<summary> Add test item data for database </summary>  
    public void AddItem(ModuleMod item)
    {
        playerDatabase.AddData(item);
        itemWorkshopUI.GenerateInventoryItemUI<ModuleInventoryDefinition>(m_ModulePrefab, ref currInventoryItem);
        currentInventory = CurrentInventory.Module;
    }

    public void AddItem(Weapon item)
    {
        playerDatabase.AddData(item);
        itemWorkshopUI.GenerateInventoryItemUI<WeaponInventoryDefinition>(m_WeaponPrefab, ref currInventoryItem);
        currentInventory = CurrentInventory.Weapon;
    }

    public void ClearItem()
    {
        playerDatabase.ClearItem();
        playerShipConfig.ClearItem();
    }

    ///<summary> 
    /// select category to display item 
    /// อาจจะทำเป็น force generate สำหรับ select display ตอนนี้จะใช้งาน method ตัวนี้จะติดปัญหาเรื่อง inventory type
    /// </summary>  
    public void SelectModule()
    {
        if (currentInventory == CurrentInventory.Weapon)
        {
            itemWorkshopUI.GenerateInventoryItemUI<ModuleInventoryDefinition>(m_ModulePrefab, ref currInventoryItem);
            currentInventory = CurrentInventory.Module;
        }
    }

    public void SelectWeapon()
    {
        if (currentInventory == CurrentInventory.Module)
        {
            itemWorkshopUI.GenerateInventoryItemUI<WeaponInventoryDefinition>(m_WeaponPrefab, ref currInventoryItem);
            currentInventory = CurrentInventory.Weapon;
        }
    }


    ///<summary> Dealing with item data and generate ui by equip and unequip </summary>  
    public void EquipModule(int id, ModuleMod mod, ItemBehaviour itemBehaviour) // send data inventory to shipconfig
    {
        currentInventory = CurrentInventory.Module;
        TranferData(id, itemBehaviour, mod);
        RemoveUI(ref currInventoryItem, id);
        itemWorkshopUI.GenerateEquipmentItemUI(m_ModulePrefab, m_WeaponPrefab, ref currEquipmentModule, ref currEquipmentWeapon);
        equipItem?.Invoke(); // invoke เพื่อ apply stat จาก statController
    }

    public void UnEquipModule(int id, ModuleMod mod, ItemBehaviour itemBehaviour) // send data shipconfig to database
    {
        SelectModule();
        TranferData(id, itemBehaviour, mod);
        RemoveUI(ref currEquipmentModule, id);
        itemWorkshopUI.GenerateInventoryItemUI<ModuleInventoryDefinition>(m_ModulePrefab, ref currInventoryItem);
        unEquipModule?.Invoke(mod, id); // remove stat ด้วย event delgate
        unEquipItem?.Invoke();
    }

    public void EquipWeapon(int id, Weapon weapon, ItemBehaviour itemBehaviour, WeaponType weaponType)
    {
        TranferData(id, itemBehaviour, weapon, weaponType);
        RemoveUI(ref currInventoryItem, id);

        itemWorkshopUI.GenerateInventoryItemUI<WeaponInventoryDefinition>(m_WeaponPrefab, ref currInventoryItem);
        itemWorkshopUI.GenerateEquipmentItemUI(m_ModulePrefab, m_WeaponPrefab, ref currEquipmentModule, ref currEquipmentWeapon);
        equipItem?.Invoke();
    }

    public void UnEquipWeapon(int id, Weapon weapon, ItemBehaviour itemBehaviour, WeaponType weaponType)
    {
        SelectWeapon();
        TranferData(id, itemBehaviour, weapon, weaponType);
        RemoveUI(ref currEquipmentWeapon, id);

        itemWorkshopUI.GenerateInventoryItemUI<WeaponInventoryDefinition>(m_WeaponPrefab, ref currInventoryItem);
        itemWorkshopUI.GenerateEquipmentItemUI(m_ModulePrefab, m_WeaponPrefab, ref currEquipmentModule, ref currEquipmentWeapon);
        unEquipWeapon?.Invoke(weapon, id);
        unEquipItem?.Invoke();
    }


    ///<summary> Utility class </summary>  
    public void RemoveUI(ref List<GameObject> itemList, int id)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            IItemController controller = itemList[i].GetComponent<IItemController>(); // ใช้ Interface เพราะ weapon, module controller มี id เหมือนกัน
            if (controller.id == id)
            {
                Destroy(itemList[i]);
                itemList.RemoveAt(i);
            }
        }
    }

    void TranferData(int id, ItemBehaviour itemBehaviour, ModuleMod mod) // ย้าย Item object ui ระหว่าง inventory database and shipconfig  ของ Module
    {
        if (itemBehaviour == ItemBehaviour.Equip)
        {
            playerShipConfig.AddModule(id, mod, itemBehaviour);
            playerDatabase.playerModuleInventory = playerDatabase.playerModuleInventory.Where(x => x.id != id).ToList(); // ลบข้อมูลตัวที่ถูกเลือกใน Inventory database
        }
        else
        {
            playerDatabase.AddData(mod);
            playerShipConfig.moduleModList = playerShipConfig.moduleModList.Where(x => x.id != id).ToList(); // ลบข้อมูลตัวที่ถูกเลือกใน player ship config
        }
    }

    void TranferData(int id, ItemBehaviour itemBehaviour, Weapon weapon, WeaponType weaponType) // ย้าย Item object ui ระหว่าง inventory database and shipconfig 
    {
        if (itemBehaviour == ItemBehaviour.Equip) // Equip
        {
            if (weaponType == WeaponType.Primary)
            {
                // playerShipConfig.AddPrimaryWeapon(weapon);
                if (playerShipConfig.primaryWeapon) // swap weapon
                {
                    Weapon temp = playerShipConfig.primaryWeapon;
                    playerShipConfig.AddPrimaryWeapon(weapon);
                    playerDatabase.playerWeaponInventroy = playerDatabase.playerWeaponInventroy.Where(x => x.id != id).ToList();
                    playerDatabase.AddData(temp);
                }
                else // if weapon null
                {
                    playerShipConfig.AddPrimaryWeapon(weapon);
                    playerDatabase.playerWeaponInventroy = playerDatabase.playerWeaponInventroy.Where(x => x.id != id).ToList();
                }

            }

            else if (weaponType == WeaponType.Secondary)
            {
                // playerShipConfig.AddSecondaryWeapon(weapon);
                if (playerShipConfig.secondaryWeapon) // swap weapon
                {
                    Weapon temp = playerShipConfig.secondaryWeapon;
                    playerShipConfig.AddSecondaryWeapon(weapon);
                    playerDatabase.playerWeaponInventroy = playerDatabase.playerWeaponInventroy.Where(x => x.id != id).ToList();
                    playerDatabase.AddData(temp);
                }
                else // if weapon null
                {
                    playerShipConfig.AddSecondaryWeapon(weapon);
                    playerDatabase.playerWeaponInventroy = playerDatabase.playerWeaponInventroy.Where(x => x.id != id).ToList();
                }
            }

            // playerDatabase.playerWeaponInventroy = playerDatabase.playerWeaponInventroy.Where(x => x.id != id).ToList(); // ลบข้อมูลตัวที่ถูกเลือกใน Inventory database  
        }
        else // UnEquip
        {
            if (weaponType == WeaponType.Primary)
            {
                // playerDatabase.AddData(weapon);
                playerShipConfig.primaryWeapon = null;
            }

            else if (weaponType == WeaponType.Secondary)
            {
                // playerDatabase.AddData(weapon);
                playerShipConfig.secondaryWeapon = null;
            }
            playerDatabase.AddData(weapon);
            // playerShipConfig.moduleModList = playerShipConfig.moduleModList.Where(x => x.id != id).ToList(); // ลบข้อมูลตัวที่ถูกเลือกใน player ship config
        }
    }
}
