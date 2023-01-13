using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemBehaviour
{
    Equip,
    Remove,
}

public class ModuleModController : MonoBehaviour
{

    // Start is called before the first frame update
    // [Header("Add Remove module to ship")]
    // [Space(10)]
    public ModuleMod mod;
    public ItemBehaviour itemBehaviour;

    void Start()
    {
    }

    void Update()
    {

    }

    public void HandleItemBehaviour()
    {
        if (itemBehaviour == ItemBehaviour.Equip)
        {
            // draw ui to PlayerShipConfig
            // move module to playerShipConfig and remove module from database
            // set ItemBehaviour to remove
        }
        else if (itemBehaviour == ItemBehaviour.Remove)
        {
            // move module to module from database  and remove playerShipConfig
            // set ItemBehaviour to Equip
        }
    }
}

[System.Serializable]
public struct MyStruct
{
    public int a;
    public string b;
}