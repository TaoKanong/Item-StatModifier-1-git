using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private ModuleModController moduleModController;
    private List<StatForModule> modStat;
    private WeaponController weaponController;
    private string itemName;

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.name == "Module-Basic(Clone)")
        {
            // Debug.Log(this.gameObject.name);
            moduleModController = GetComponent<ModuleModController>();
            itemName = moduleModController.mod.name;
            modStat = moduleModController.mod.stat;
        }
        else if (this.gameObject.name == "Weapon-Basic(Clone)")
        {
            weaponController = GetComponent<WeaponController>();
            itemName = weaponController.weapon.name;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Instance.Show(itemName, "Test Detail");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Instance.Hide();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        TooltipSystem.Instance.Hide();
    }
}
