using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum ItemCategory
// {
//     Module
// }

public enum ItemBehaviour
{
    Equip,
    Remove,
}

public class ModuleModController : MonoBehaviour, IItemController
{

    // Start is called before the first frame update
    // [Header("Add Remove module to ship")]
    // [Space(10)]
    public int id { get; set; }
    [SerializeField] int Id; // for show only
    public ModuleMod mod;
    public ItemBehaviour itemBehaviour;

    void Start()
    {
        Id = id;
    }

    void Update()
    {

    }

    public void HandleItemBehaviour()
    {
        if (itemBehaviour == ItemBehaviour.Equip) // Equip to PlayerConfig
        {
            InventorySystem.Instance.EquipModule(id, mod, itemBehaviour);
            // draw ui to PlayerShipConfig
            // move module to playerShipConfig and remove module from database
            // set ItemBehaviour to remove
        }
        else if (itemBehaviour == ItemBehaviour.Remove)
        {
            InventorySystem.Instance.UnEquipModule(id, mod, itemBehaviour);
            // move module to module from database  and remove playerShipConfig
            // set ItemBehaviour to Equip
        }
    }

    void RemoveItem()// remove by id
    {

    }
}

[System.Serializable]
public struct MyStruct
{
    public int a;
    public string b;
}