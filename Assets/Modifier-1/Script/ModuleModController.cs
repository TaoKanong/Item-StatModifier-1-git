using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleModController : MonoBehaviour
{

    // Start is called before the first frame update
    [Header("Add Remove module to ship")]
    [Space(10)]
    public List<ModuleMod> moduleModList;
    public StatDatabase m_StatDatabase;
    // private GameObject player;
    private StatController statController;
    void Start()
    {
        // statController = GetComponent<StatController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}


[System.Serializable]
public struct MyStruct
{
    public int a;
    public string b;
}