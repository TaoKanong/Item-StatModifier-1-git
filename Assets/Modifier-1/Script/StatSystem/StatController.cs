using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StatController : Singleton<StatController>
{
    [SerializeField] private StatDatabase m_StatDatabase;
    [SerializeField] Dictionary<string, Stat> m_Stats = new Dictionary<string, Stat>(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, Stat> stats => m_Stats;
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
        }

        foreach (StatDefinition definition in m_StatDatabase.attributes)
        {
            m_Stats.Add(definition.name, new Attribute(definition));
        }

        foreach (StatDefinition definition in m_StatDatabase.primaryStats)
        {
            m_Stats.Add(definition.name, new PrimaryStat(definition));
        }

        ApplyStatModule();

    }

    private void OnEnable()
    {
        InventorySystem.Instance.equipModule += ApplyStatModule;
        InventorySystem.Instance.unEquipModule += RemoveStatModule;
    }

    private void OnDisable()
    {
        InventorySystem.Instance.equipModule -= ApplyStatModule;
        InventorySystem.Instance.unEquipModule -= RemoveStatModule;
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

    void OnGUI()
    {
        GUI.color = Color.green;

        GUI.Label(
            new Rect(125, 0, 200, 20),
            "Physic Attack: " + stats["PhysicalAttack"].value);

        GUI.Label(
            new Rect(125, 15, 200, 20),
            "AttackSpeed: " + stats["AttackSpeed"].value);
    }
}
