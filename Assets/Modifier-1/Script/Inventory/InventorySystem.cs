using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    // Start is called before the first frame update
    public ModuleInventoryDatabase playerModuleDatabase;
    public GameObject modulePrefab;
    void Start()
    {
        foreach (ModuleMod mod in playerModuleDatabase.playerModuleInventory)
        {
            GameObject obj = new GameObject("AA");
            obj.transform.SetParent(this.transform, this.transform.parent);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }



}
