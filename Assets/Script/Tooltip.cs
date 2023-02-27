using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    [SerializeField] private float tooltipSpacing;
    public object itemController;
    public TMP_Text itemName;
    public TMP_Text statDetail;
    private RectTransform rectTransform;

    public void SetText(string itemName, string detail)
    {
        this.itemName.text = itemName;
    }

    public void SetPosition()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }

        Vector2 position = Input.mousePosition;

        float PivotX = position.x / Screen.width;
        float PivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(PivotX, PivotY);
        transform.position = position;
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
