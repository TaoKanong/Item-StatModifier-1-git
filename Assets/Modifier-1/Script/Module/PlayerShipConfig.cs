using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatSystem/PlayerShipConfig", fileName = "PlayerShipConfig", order = 0)]
public class PlayerShipConfig : ScriptableObject
{
    public Weapon primaryWeapon;
    public Weapon secondaryWeapon;
    public List<ModuleInventoryDefinition> moduleModList;
    // public List<>

    public void AddModule(int id, ModuleMod newMod, ItemBehaviour itemBehaviour)
    {
        // ต้อง check slot ของ ยานก่อน Add ด้วย
        // moduleModList.Add(new ModuleInventoryDefinition
        // {
        //     id = id,
        //     mod = mod,
        //     itemBehaviour = ItemBehaviour.Remove
        // });

        if (moduleModList.Count == 0)
        {
            moduleModList.Add(new ModuleInventoryDefinition
            {
                id = 0,
                mod = newMod,
                itemBehaviour = ItemBehaviour.Remove
            });
        }
        else
        {
            ModuleInventoryDefinition lastElement = moduleModList.Last();
            int newId = lastElement.id + 1;

            moduleModList.Add(new ModuleInventoryDefinition
            {
                id = newId,
                mod = newMod,
                itemBehaviour = ItemBehaviour.Remove
            });
        }
    }

    public void AddPrimaryWeapon(Weapon weapon)
    {
        primaryWeapon = weapon;
        // if(primaryWeapon == null)
        // {
        //     primaryWeapon = weapon;
        // }
    }

    public void AddSecondaryWeapon(Weapon weapon)
    {
        secondaryWeapon = weapon;
    }

    public void AddData(ModuleMod newMod)
    {

    }

    public void ClearItem()
    {
        moduleModList.Clear();
        primaryWeapon = null;
        secondaryWeapon = null;
    }
}
