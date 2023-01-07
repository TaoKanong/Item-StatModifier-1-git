using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatSystem/ModuleDatabase", fileName = "ModuleDatabase", order = 0)]
public class ModuleInventoryDatabase : ScriptableObject
{
    public List<ModuleMod> playerModuleInventory;
}
