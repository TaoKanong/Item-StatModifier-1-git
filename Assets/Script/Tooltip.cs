using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    public object itemController;
    public TMP_Text itemName;
    public TMP_Text statDetail;

    public void SetText(string itemName, string detail)
    {
        this.itemName.text = itemName;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
