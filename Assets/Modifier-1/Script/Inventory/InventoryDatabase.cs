using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatSystem/InventoryDatabase", fileName = "InventoryDatabase", order = 0)]
public class InventoryDatabase : ScriptableObject
{
    public List<ModuleInventoryDefinition> playerModuleInventory;
    public List<WeaponInventoryDefinition> playerWeaponInventroy;
    public void AddData(ModuleMod newMod)
    {
        if (playerModuleInventory.Count == 0)
        {
            playerModuleInventory.Add(new ModuleInventoryDefinition
            {
                id = 0,
                mod = newMod
            });
        }
        else
        {
            ModuleInventoryDefinition lastElement = playerModuleInventory.Last();
            int newId = lastElement.id + 1;

            playerModuleInventory.Add(new ModuleInventoryDefinition
            {
                id = newId,
                mod = newMod
            });
        }
    }

    public void AddData(Weapon newWeapon) // overload method for weapon
    {
        if (playerWeaponInventroy.Count == 0)
        {
            playerWeaponInventroy.Add(new WeaponInventoryDefinition
            {
                id = 0,
                weapon = newWeapon
            });
        }
        else
        {
            WeaponInventoryDefinition lastElement = playerWeaponInventroy.Last();
            int newId = lastElement.id + 1;

            playerWeaponInventroy.Add(new WeaponInventoryDefinition
            {
                id = newId,
                weapon = newWeapon
            });
            // item behavior จะถูก set ที่ ItemWorkshopUI ตอนที่ถูก instantiate ขึ้นมา
        }
    }

    public void SwapWeapon(Weapon newWeapon)
    {

    }
}

[System.Serializable]
public class ModuleInventoryDefinition
{
    public int id;
    public ModuleMod mod;
    public ItemBehaviour itemBehaviour;
}

[System.Serializable]
public class WeaponInventoryDefinition
{
    public int id;
    public Weapon weapon;
    public ItemBehaviour itemBehaviour;
}