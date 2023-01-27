using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataHandler
{
    private PlayerShipConfig m_PlayerShipConfig;
    private InventoryDatabase m_InventoryDatabase;

    public void SaveData(InventoryDatabase inventoryDatabase, PlayerShipConfig playerShipConfig)
    {
        PlayerPrefs.SetString("playerModuleInventory", JsonUtility.ToJson(inventoryDatabase.playerModuleInventory));
        PlayerPrefs.SetString("playerWeaponInventroy", JsonUtility.ToJson(inventoryDatabase.playerWeaponInventroy));

        PlayerPrefs.SetString("primaryWeapon", JsonUtility.ToJson(playerShipConfig.primaryWeapon));
        PlayerPrefs.SetString("secondaryWeapon", JsonUtility.ToJson(playerShipConfig.secondaryWeapon));
        PlayerPrefs.SetString("moduleModList", JsonUtility.ToJson(playerShipConfig.moduleModList));

        PlayerPrefs.Save();
    }

    // public void LoadData(InventoryDatabase inventoryDatabase, PlayerShipConfig playerShipConfig)
    // {
    //     inventoryDatabase.playerModuleInventory = JsonUtility.FromJson<List<ModuleInventoryDefinition>>(PlayerPrefs.GetString("playerModuleInventory"));
    //     inventoryDatabase.playerWeaponInventroy = JsonUtility.FromJson<List<WeaponInventoryDefinition>>(PlayerPrefs.GetString("playerWeaponInventroy"));

    //     playerShipConfig.primaryWeapon = JsonUtility.FromJson<Weapon>(PlayerPrefs.GetString("primaryWeapon"));
    //     playerShipConfig.secondaryWeapon = JsonUtility.FromJson<Weapon>(PlayerPrefs.GetString("secondaryWeapon"));
    //     playerShipConfig.moduleModList = JsonUtility.FromJson<List<ModuleInventoryDefinition>>(PlayerPrefs.GetString("moduleModList"));
    // }

    // public void DebugData(InventoryDatabase playerDatabase)
    // {
    //     InventoryDatabase myObject = ScriptableObject.CreateInstance<InventoryDatabase>();
    //     myObject = playerDatabase;
    //     string json = JsonUtility.ToJson(myObject);

    //     JsonUtility.FromJsonOverwrite(json, myObject);
    // }

    // public void LoadV2(string savingData, InventoryDatabase playerDatabase)
    // {
    //     JsonUtility.FromJsonOverwrite(savingData, playerDatabase);
    // }
}
