using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatSystem/Database", fileName = "Database", order = 0)]
public class InventoryDatabase : ScriptableObject
{
    public List<ModuleInventoryDefinition> playerModuleInventory;
    public List<ModuleInventoryDefinition> playerWeaponInventroy;
    public List<WeaponInventoryDefinition> testForWeaponInventory;
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

    // public void AddData(Weapon newMod) // overload method for weapon
    // {
    //     playerWeaponInventroy.Add(new ModuleInventoryDatabaseDefinition
    //     {
    //         id = GenerateID(),
    //         mod = newMod
    //     });
    // }
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
    public ModuleMod mod;
    public ItemBehaviour itemBehaviour;
}