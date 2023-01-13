using System.Collections;
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
    private ModuleModController moduleModController;
    public List<GameObject> currItem = new List<GameObject>();
    public CurrentInventory currentInventory;
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
        currentInventory = CurrentInventory.Module;
        foreach (ModuleInventoryDatabaseDefinition mod in playerDatabase.playerModuleInventory)
        {
            GameObject newObj = Instantiate(m_ModulePrefab);
            moduleModController = newObj.GetComponent<ModuleModController>();
            moduleModController.mod = mod.mod;
            moduleModController.itemBehaviour = ItemBehaviour.Equip;

            currItem.Add(newObj);
            newObj.transform.SetParent(m_InventoryUI.transform);
            ResizeToStandard(newObj);
        }
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

    void ClearUIGameObject()
    {
        if (currItem.Count > 0)
        {
            for (int i = 0; i < currItem.Count; i++)
            {
                Destroy(currItem[i]);
            }
            currItem.Clear();
        }
    }

    void GenerateModuleItemUI()
    {
        ClearUIGameObject();
        foreach (ModuleInventoryDatabaseDefinition mod in playerDatabase.playerModuleInventory)
        {
            GameObject newObj = Instantiate(m_ModulePrefab);
            moduleModController = newObj.GetComponent<ModuleModController>();
            moduleModController.mod = mod.mod;
            moduleModController.itemBehaviour = ItemBehaviour.Equip;

            currItem.Add(newObj);
            newObj.transform.SetParent(m_InventoryUI.transform, m_InventoryUI.transform.parent);
            ResizeToStandard(newObj);
        }
    }

    void GenerateWeaponItemUI()
    {
        ClearUIGameObject();
        foreach (ModuleInventoryDatabaseDefinition mod in playerDatabase.playerWeaponInventroy)
        {
            GameObject newObj = Instantiate(m_ModulePrefab);
            moduleModController = newObj.GetComponent<ModuleModController>();
            moduleModController.mod = mod.mod;
            moduleModController.itemBehaviour = ItemBehaviour.Equip;

            currItem.Add(newObj);
            newObj.transform.SetParent(m_InventoryUI.transform, m_InventoryUI.transform.parent);
            ResizeToStandard(newObj);
        }
    }

    void ResizeToStandard(GameObject obj)
    {
        obj.transform.localScale = new Vector3(1, 1, 1);
    }
}
