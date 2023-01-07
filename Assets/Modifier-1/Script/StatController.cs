using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StatController : MonoBehaviour
{
    [SerializeField] private StatDatabase m_StatDatabase;
    [SerializeField] Dictionary<string, Stat> m_Stats = new Dictionary<string, Stat>(StringComparer.OrdinalIgnoreCase);
    public Dictionary<string, Stat> stats => m_Stats;
    [SerializeField] private bool m_IsInitialized;
    public bool isInitialized => m_IsInitialized;
    public event Action initialized;
    public event Action unInitialized;
    // ModuleModController moduleModController;
    public PlayerShipConfig playerShipConfig;
    public PlayerShipConfig test;
    // Start is called before the first frame update

    protected virtual void Awake()
    {
        if (!m_IsInitialized)
        {
            Initialize();
            m_IsInitialized = true;
            initialized?.Invoke();
        }

        if(test == null)
        {
            Debug.Log("null");
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
    }
    void Start()
    {
        // foreach (ModuleMod mod in moduleModController.moduleModList)
        // {
        //     foreach (StatForModule modStat in mod.stat)
        //     {
        //         Debug.Log(modStat.statName);
        //     }
        //     // Debug.Log(mod.stat);
        // }
        // EquipModule();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EquipModule() // Apply List module to statmodifier
    {
        // Debug.Log(moduleModController.moduleModList.Count);
        // float value = 0;
        // if (moduleModController.m_StatDatabase == null)
        // {
        //     Debug.Log("Null11");
        // }
        foreach (ModuleMod mod in test.moduleModList)
        {
            Debug.Log("A");
            foreach (StatForModule modStat in mod.stat)
            {
                Debug.Log("B");
                foreach (KeyValuePair<string, Stat> pair in stats) // ใช้ใน ModuleMod ตัว Module mod จะให้ใส่ Stat scriptableObject ลงไป อยากเพิ่มตัวไหนก็ใส่ลงไป 
                {
                    Debug.Log("C");
                    string key = pair.Key;
                    Stat stat = stats[key];
                    if (modStat.statName == pair.Key)
                    {
                        // modStat.Source = RandomGameObject();
                        stat.AddModifier(new StatModifier
                        {
                            source = RandomGameObject(),
                            magnitude = modStat.magnitude,
                            Type = ModifierOperationType.Additive
                        });
                        // value = modStat.magnitude;
                        break;
                    }
                }
            }
        }

        // for (int i = 0; i < moduleModController.moduleModList.Count; i++)
        // {
        //     for (int j = 0; j < moduleModController.moduleModList[i].stat.Count; j++)
        //     {
        //         foreach (KeyValuePair<string, Stat> pair in stats) // ใช้ใน ModuleMod ตัว Module mod จะให้ใส่ Stat scriptableObject ลงไป อยากเพิ่มตัวไหนก็ใส่ลงไป 
        //         {
        //             string key = pair.Key;
        //             Stat stat = stats[key];

        //             string modStat = moduleModController.moduleModList[i].stat[j].statName;
        //             float modMagnitude = moduleModController.moduleModList[i].stat[j].magnitude;
        //             if (modStat == pair.Key)
        //             {
        //                 // modStat.Source = RandomGameObject();
        //                 stat.AddModifier(new StatModifier
        //                 {
        //                     source = RandomGameObject(),
        //                     magnitude = modMagnitude,
        //                     Type = ModifierOperationType.Additive
        //                 });
        //             }
        //         }
        //     }
        // }


    }


    // public void EquipModule()
    // {
    //     foreach (KeyValuePair<string, Stat> pair in stats) // ใช้ใน ModuleMod ตัว Module mod จะให้ใส่ Stat scriptableObject ลงไป อยากเพิ่มตัวไหนก็ใส่ลงไป 
    //     {
    //         string key = pair.Key;
    //         Stat stat = stats[key];
    //         // Stat stat = pair.Value;

    //         foreach (ModuleMod mod in moduleModController.moduleModList)
    //         {
    //             // Debug.Log("A");
    //             foreach (StatForModule modStat in mod.stat)
    //             {
    //                 // Debug.Log("B");
    //                 if (modStat.statName == pair.Key)
    //                 {
    //                     // modStat.source = RandomGameObject().name;
    //                     stat.AddModifier(new StatModifier
    //                     {
    //                         source = RandomGameObject(),
    //                         magnitude = modStat.magnitude,
    //                         Type = ModifierOperationType.Additive
    //                     });
    //                 }
    //             }
    //         }
    //     }
    // }

    // public void RemoveModule()
    // {
    //     foreach (KeyValuePair<string, Stat> pair in stats) // ใช้ใน ModuleMod ตัว Module mod จะให้ใส่ Stat scriptableObject ลงไป อยากเพิ่มตัวไหนก็ใส่ลงไป 
    //     {
    //         string key = pair.Key;
    //         Stat stat = stats[key];

    //         foreach (ModuleMod mod in moduleModController.moduleModList)
    //         {
    //             foreach (StatForModule modStat in mod.stat)
    //             {
    //                 if (modStat.statName == pair.Key)
    //                 {
    //                     // modStat.Source = RandomGameObject();
    //                     stat.AddModifier(new StatModifier
    //                     {
    //                         // source = modStat.Source,
    //                         magnitude = modStat.magnitude,
    //                         Type = ModifierOperationType.Additive
    //                     });
    //                 }
    //             }
    //         }
    //     }
    // }

    GameObject RandomGameObject()
    {
        GameObject c = new GameObject("abc" + RandomNumber().ToString("D3"));
        return c;
    }

    int RandomNumber()
    {
        int randNum = Random.Range(0, 998);
        return randNum;
    }
}
