using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour, IItemController
{
    // Start is called before the first frame update
    public int id { get; set; }
    [SerializeField] int Id;
    public Weapon weapon;
    public WeaponType weaponType;
    public ItemBehaviour itemBehaviour;
    void Start()
    {
        Id = id;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleItemBehaviour()
    {
        if (itemBehaviour == ItemBehaviour.Equip) // Equip to PlayerConfig
        {
            InventorySystem.Instance.EquipWeapon(id, weapon, itemBehaviour, weaponType);
            // draw ui to PlayerShipConfig
            // move module to playerShipConfig and remove module from database
            // set ItemBehaviour to remove
        }
        else if (itemBehaviour == ItemBehaviour.Remove)
        {
            InventorySystem.Instance.UnEquipWeapon(id, weapon, itemBehaviour, weaponType);
            // move module to module from database  and remove playerShipConfig
            // set ItemBehaviour to Equip
        }
    }
}
