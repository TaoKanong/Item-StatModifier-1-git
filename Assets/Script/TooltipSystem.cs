using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public static TooltipSystem Instance;
    [SerializeField] private Tooltip tooltip;
    void Start()
    {
        Instance = this;
    }

    public void Show(string itemName, string detail)
    {
        tooltip.SetText(itemName, "Test detail");
        tooltip.gameObject.SetActive(true);
    }

    public void Hide()
    {
        tooltip.gameObject.SetActive(false);
    }
}
