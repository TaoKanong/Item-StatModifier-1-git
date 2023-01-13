using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatSystem/Database", fileName = "Database", order = 0)]
public class InventoryDatabase : ScriptableObject
{
    public List<ModuleInventoryDatabaseDefinition> playerModuleInventory;
    public List<ModuleInventoryDatabaseDefinition> playerWeaponInventroy;
    public void AddData(ModuleMod newMod)
    {
        if (playerModuleInventory.Count == 0)
        {
            playerModuleInventory.Add(new ModuleInventoryDatabaseDefinition
            {
                id = 0,
                mod = newMod
            });
        }
        else
        {
            ModuleInventoryDatabaseDefinition lastElement = playerModuleInventory.Last();
            int newId = lastElement.id + 1;

            playerModuleInventory.Add(new ModuleInventoryDatabaseDefinition
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

    private int GenerateID()
    {
        if (playerModuleInventory.Count == 0)
        {
            int finalValue = 0 + 1;
            int newValue = 0;
            for (int i = 0; i < playerModuleInventory.Count; i++)
            {
                int currID = playerModuleInventory[i].id;
                int nextID = playerModuleInventory[i + 1].id;

                if (currID < nextID)
                {
                    newValue = nextID;
                }

                if (currID > newValue)
                {
                    finalValue = currID;
                }

            }
            return finalValue;
        }
        return 0;
    }
}

[System.Serializable]
public class ModuleInventoryDatabaseDefinition
{
    public int id;
    public ModuleMod mod;
}

[System.Serializable]
public class WeaponInventoryDatabaseDefinition
{
    public int id;
    public ModuleMod mod;
}