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
    [Header("UI Prefab for module")]
    [Space(10)]
    [SerializeField] private GameObject m_ModulePrefab;
    [Space(15)]
    [Header("UI Gameobject")]
    [Space(10)]
    [SerializeField] private GameObject m_InventoryUI;
    [SerializeField] private GameObject m_WeaponEquipment;
    [SerializeField] private GameObject m_ModuleEquipmentUI;
    [SerializeField] private List<GameObject> m_Postion = new List<GameObject>();
    private ModuleModController moduleModController;
    public List<GameObject> currInventoryItem = new List<GameObject>();
    public List<GameObject> currEquipmentItem = new List<GameObject>();
    public CurrentInventory currentInventory; // item topic => module, weapon
    public event Action equipModule;
    public delegate void RemoveStatModule(ModuleMod moduleMod, int id); // ใช้กับ Statcontroller remove stat module
    public event RemoveStatModule unEquipModule;
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
        itemWorkshopUI = new ItemWorkshopUI(playerDatabase, playerShipConfig, m_Postion);
        GenerateModuleItemUI();
        GenerateEquipModuleItemUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(ModuleMod item)
    {
        playerDatabase.AddData(item);
        GenerateModuleItemUI();
    }

    // public void AddItem(Weapon item)
    // {
    //     playerDatabase.AddData(item);
    // }

    public void SelectModule()
    {
        if (currentInventory == CurrentInventory.Weapon)
        {
            GenerateModuleItemUI();
            currentInventory = CurrentInventory.Module;
        }
    }

    public void SelectWeapon()
    {
        if (currentInventory == CurrentInventory.Module)
        {
            GenerateWeaponItemUI();
            currentInventory = CurrentInventory.Weapon;
        }
    }

    void GenerateModuleItemUI() // แยกเพราะมีหัวข้อให้เลือกแสดงข้อมูล แสดงข้อมูลไม่ได้พร้อมกัน
    {
        int idx = 0;
        // int idx2 = 1;

        RefreshItemUI(currInventoryItem);
        while (idx < playerDatabase.playerModuleInventory.Count)
        {
            itemWorkshopUI.GenerateInventoryItemUI<ModuleInventoryDefinition>(InstantiatePrefabItemUI(m_ModulePrefab), idx + 1);
            idx++;
        }
        currInventoryItem = itemWorkshopUI.currInventoryItem;

        // while (idx2 < playerDatabase.playerModuleInventory.Count) 
        // {
        //     currInventoryItem.Add(InstantiatePrefabItemUI(m_ModulePrefab));
        //     idx2++;
        // }
        // itemWorkshopUI.GenerateInventoryItemUI<ModuleInventoryDefinition>(currInventoryItem);
    }

    void GenerateWeaponItemUI()
    {
        int idx = 0;

        RefreshItemUI(currInventoryItem);
        while (idx < playerDatabase.playerWeaponInventroy.Count)
        {
            itemWorkshopUI.GenerateInventoryItemUI<WeaponInventoryDefinition>(InstantiatePrefabItemUI(m_ModulePrefab), idx + 1);
            idx++;
        }
        currInventoryItem = itemWorkshopUI.currInventoryItem;
    }

    void GenerateEquipModuleItemUI() // เอาของ weapon มารวมกันได้เลย
    {
        RefreshItemUI(currEquipmentItem);
        for (int i = 0; i < playerShipConfig.moduleModList.Count; i++)
        {
            itemWorkshopUI.GenerateEquipmentItemUI<ModuleInventoryDefinition>(InstantiatePrefabItemUI(m_ModulePrefab), i + 1);
        }
        currEquipmentItem = itemWorkshopUI.currEquipmentItem;
    }

    public void EquipModule(int id, ModuleMod mod, ItemBehaviour itemBehaviour) // send data inventory to shipconfig
    {
        currentInventory = CurrentInventory.Module;
        playerShipConfig.AddModule(id, mod, itemBehaviour);
        TranferData(id, itemBehaviour);
        RemoveUI(ref currInventoryItem, id);
        // GenerateModuleItemUI();
        GenerateEquipModuleItemUI();
        equipModule?.Invoke(); // invoke เพื่อ apply stat จาก statController
    }

    public void UnEquipModule(int id, ModuleMod mod, ItemBehaviour itemBehaviour) // send data shipconfig to database
    {
        SelectModule();
        playerDatabase.AddData(mod);
        TranferData(id, itemBehaviour);
        RemoveUI(ref currEquipmentItem, id);
        GenerateModuleItemUI();
        unEquipModule(mod, id); // remove stat ด้วย event delgate
    }

    void RefreshItemUI(List<GameObject> list)
    {
        if (list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Destroy(list[i]);
            }
            list.Clear();
        }
    }

    public void RemoveUI(ref List<GameObject> itemList, int id)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            ModuleModController controller = itemList[i].GetComponent<ModuleModController>();
            if (controller.id == id)
            {
                Destroy(itemList[i]);
                itemList.RemoveAt(i);
            }
        }
    }

    void TranferData(int id, ItemBehaviour itemBehaviour) // ย้าย Item object ui ระหว่าง inventory database and shipconfig 
    {
        if (itemBehaviour == ItemBehaviour.Equip)
        {
            playerDatabase.playerModuleInventory = playerDatabase.playerModuleInventory.Where(x => x.id != id).ToList();
            // GenerateModuleItemUI();
        }
        else
        {
            playerShipConfig.moduleModList = playerShipConfig.moduleModList.Where(x => x.id != id).ToList();
            // GenerateModuleItemUI();
        }
    }

    public GameObject InstantiatePrefabItemUI(GameObject obj)
    {
        GameObject newObj = Instantiate(obj);
        return newObj;
    }

    void ResizeToStandard(GameObject obj)
    {
        obj.transform.localScale = new Vector3(1, 1, 1);
    }
}
