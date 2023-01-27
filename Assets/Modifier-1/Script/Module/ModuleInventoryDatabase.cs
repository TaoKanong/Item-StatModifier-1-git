// using System.Collections;
// using System.Linq;
// using System.Collections.Generic;
// using UnityEngine;

// [CreateAssetMenu(menuName = "StatSystem/ModuleDatabase", fileName = "ModuleDatabase", order = 0)]
// public class ModuleInventoryDatabase : ScriptableObject
// {
//     public List<ModuleInventoryDatabaseDefinition> playerModuleInventory;
//     public List<ModuleInventoryDatabaseDefinition> playerWeaponInventroy;
//     public void Add(ModuleMod newMod)
//     {
//         playerModuleInventory.Add(new ModuleInventoryDatabaseDefinition
//         {
//             id = GenerateID(),
//             mod = newMod
//         });
//     }

//     private int GenerateID()
//     {
//         if (playerModuleInventory.Count == 0)
//         {
//             int finalValue = 0 + 1;
//             int newValue = 0;
//             for (int i = 0; i < playerModuleInventory.Count; i++)
//             {
//                 int currID = playerModuleInventory[i].id;
//                 int nextID = playerModuleInventory[i + 1].id;

//                 if (currID < nextID)
//                 {
//                     newValue = nextID;
//                 }

//                 if (currID > newValue)
//                 {
//                     finalValue = currID;
//                 }

//             }
//             return finalValue;
//         }
//         return 0;
//     }
// }

// [System.Serializable]
// public class ModuleInventoryDatabaseDefinition
// {
//     public int id;
//     public ModuleMod mod;
// }

// int currID = playerModuleInventory[i].id;
// int nextID = playerModuleInventory[i + 1].id;

// if (currID < nextID)
// {
//     newValue = nextID;
//     return newValue;
// }
