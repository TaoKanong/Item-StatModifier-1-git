using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StatController : Singleton<StatController>//, ISavable
{
    [SerializeField] private StatDatabase m_StatDatabase;
    [SerializeField] Dictionary<string, Stat> m_Stats = new Dictionary<string, Stat>(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, Stat> stats => m_Stats;

    // Dictionary<string, Stat> m_BaseStats = new Dictionary<string, Stat>(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, Stat> baseStats = new Dictionary<string, Stat>(StringComparer.OrdinalIgnoreCase);
    [SerializeField] private bool m_IsInitialized;
    public bool isInitialized => m_IsInitialized;
    public event Action initialized;
    public event Action unInitialized;
    // ModuleModController moduleModController;
    [SerializeField] private PlayerShipConfig m_PlayerShipConfig;
    public PlayerShipConfig playerShipConfig => m_PlayerShipConfig;

    protected override void Awake()
    {
        base.Awake();
        if (!m_IsInitialized)
        {
            Initialize();
            m_IsInitialized = true;
            initialized?.Invoke();
        }
    }

    private void OnDestroy()
    {
        unInitialized?.Invoke();
    }

    void Initialize()
    {
        // moduleModController = GetComponent<ModuleModController>();
        foreach (StatDefinition definition in m_StatDatabase.stat)
        {
            m_Stats.Add(definition.name, new Stat(definition));
            baseStats.Add(definition.name, new Stat(definition));
        }

        foreach (StatDefinition definition in m_StatDatabase.attributes)
        {
            m_Stats.Add(definition.name, new Attribute(definition));
            baseStats.Add(definition.name, new Attribute(definition));
        }

        foreach (StatDefinition definition in m_StatDatabase.primaryStats)
        {
            m_Stats.Add(definition.name, new PrimaryStat(definition));
            baseStats.Add(definition.name, new PrimaryStat(definition));
        }
        ApplyStatModule();
    }

    private void OnEnable()
    {
        if (InventorySystem.Instance != null)
        {
            InventorySystem.Instance.equipItem += ApplyStatModule;
            InventorySystem.Instance.unEquipModule += RemoveStatModule;

            InventorySystem.Instance.unEquipWeapon += RemoveStatWeapon;
        }
    }

    private void OnDisable()
    {
        if (InventorySystem.Instance != null)
        {
            InventorySystem.Instance.equipItem -= ApplyStatModule;
            InventorySystem.Instance.unEquipModule -= RemoveStatModule;

            InventorySystem.Instance.unEquipWeapon -= RemoveStatWeapon;
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void ApplyStatModule() // Apply List module to statmodifier
    {
        foreach (KeyValuePair<string, Stat> pair in stats) // clear stat ค้างก่อนที่จะ apply stat ใหม่
        {
            // Debug.Log("stats");
            string key = pair.Key;
            Stat stat = stats[key];

            stat.ClearModifier();
        }

        foreach (ModuleInventoryDefinition modList in playerShipConfig.moduleModList)
        {
            // Debug.Log("moduleModList");
            foreach (StatForModule modStat in modList.mod.stat)
            {
                // Debug.Log("mod.stat");
                foreach (KeyValuePair<string, Stat> pair in stats) // ใช้ใน ModuleMod ตัว Module mod จะให้ใส่ Stat scriptableObject ลงไป อยากเพิ่มตัวไหนก็ใส่ลงไป 
                {
                    // Debug.Log("stats");
                    string key = pair.Key;
                    Stat stat = stats[key];

                    if (modStat.statName == pair.Key)
                    {
                        stat.AddModifier(new StatModifier
                        {
                            id = modList.id,
                            magnitude = modStat.magnitude,
                            Type = ModifierOperationType.Additive
                        });
                        break;
                    }
                }
            }
        }

        if (playerShipConfig.primaryWeapon != null)
        {
            foreach (WeaponStatDefinition weapon in playerShipConfig.primaryWeapon.statList)
            {
                string statName = weapon.statName;
                float magnitude = weapon.magnitude;

                foreach (KeyValuePair<string, Stat> pair in stats) // ใช้ใน ModuleMod ตัว Module mod จะให้ใส่ Stat scriptableObject ลงไป อยากเพิ่มตัวไหนก็ใส่ลงไป 
                {
                    // Debug.Log("stats");
                    string key = pair.Key;
                    Stat stat = stats[key];

                    if (statName == pair.Key)
                    {
                        stat.AddModifier(new StatModifier
                        {
                            weaponId = 0,
                            magnitude = magnitude,
                            Type = ModifierOperationType.Additive
                        });
                        break;
                    }
                }
            }
        }

        if (playerShipConfig.secondaryWeapon != null)
        {
            foreach (WeaponStatDefinition weapon in playerShipConfig.secondaryWeapon.statList)
            {
                string statName = weapon.statName;
                float magnitude = weapon.magnitude;

                foreach (KeyValuePair<string, Stat> pair in stats) // ใช้ใน ModuleMod ตัว Module mod จะให้ใส่ Stat scriptableObject ลงไป อยากเพิ่มตัวไหนก็ใส่ลงไป 
                {
                    // Debug.Log("stats");
                    string key = pair.Key;
                    Stat stat = stats[key];

                    if (statName == pair.Key)
                    {
                        stat.AddModifier(new StatModifier
                        {
                            weaponId = 1,
                            magnitude = magnitude,
                            Type = ModifierOperationType.Additive
                        });
                        break;
                    }
                }
            }
        }

    }

    public void RemoveStatModule(ModuleMod moduleMod, int id)
    {
        foreach (StatForModule mod in moduleMod.stat)
        {
            string statName = mod.statName;

            foreach (KeyValuePair<string, Stat> pair in stats)
            {
                string key = pair.Key;
                Stat stat = stats[key];

                if (key == statName)
                {
                    stat.RemoveModifierByID(id);
                }
            }
            // Debug.Log(mod.statName);
        }
    }

    public void RemoveStatWeapon(Weapon weapon, int weaponId)
    {
        foreach (WeaponStatDefinition weaponStat in weapon.statList)
        {
            foreach (KeyValuePair<string, Stat> pair in stats)
            {
                string key = pair.Key;
                Stat stat = stats[key];

                if (key == weaponStat.statName)
                {
                    stat.RemoveWeaponModifierById(weaponId);
                }
            }
        }
    }

    void OnGUI()
    {
        GUI.color = Color.green;
        int spacing = 0;
        foreach (KeyValuePair<string, Stat> pair in stats)
        {
            GUI.Label(
                new Rect(850, spacing, 200, 20),
                pair.Key + " " + stats[pair.Key].value);
            spacing += 20;
        }
    }

    // # region Stat system // เก็บไว้ลอง

    // public object data
    // {
    //     get
    //     {
    //         Dictionary<string, object> stats = new Dictionary<string, object>();
    //         foreach (Stat stat in m_Stats.Values)
    //         {
    //             if (stat is ISavable savable)
    //             {
    //                 stats.Add(stat.definition.name, savable.data);
    //             }
    //         }
    //         return new StatControllerData
    //         {
    //             stats = stats
    //         };
    //     }
    // }
    // public void Load(object data)
    // {
    //     StatControllerData statControllerData = (StatControllerData)data;
    //     foreach (Stat stat in m_Stats.Values)
    //     {
    //         if (stat is ISavable savable)
    //         {
    //             savable.Load(statControllerData.stats[stat.definition.name]);
    //         }
    //     }
    // }

    // [Serializable]
    // protected class StatControllerData
    // {
    //     public Dictionary<string, object> stats;
    // }

    // # endregion
}
