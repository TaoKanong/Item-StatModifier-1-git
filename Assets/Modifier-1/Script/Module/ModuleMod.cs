using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

[System.Serializable]
[CreateAssetMenu(menuName = "StatSystem/Module", fileName = "Module", order = 0)]
public class ModuleMod : ScriptableObject
{
    [Header("Module StatModifier")]
    [Space(20)]
    public List<StatForModule> stat;
    public Sprite icon;  // ทำ 3 อันนี้เพิ่มภายหลัง
    // public string detail;
    // public Rarerity 
}

[System.Serializable]
public class StatForModule
{
    public StatDefinition statDefinition;
    public string statName => statDefinition.name;
    public float magnitude;
    // [ReadOnly] public string source;
}

// public class ReadOnlyAttribute : PropertyAttribute
// {

// }

// [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
// public class ReadOnlyDrawer : PropertyDrawer
// {
//     public override float GetPropertyHeight(SerializedProperty property,
//                                             GUIContent label)
//     {
//         return EditorGUI.GetPropertyHeight(property, label, true);
//     }

//     public override void OnGUI(Rect position,
//                                SerializedProperty property,
//                                GUIContent label)
//     {
//         GUI.enabled = false;
//         EditorGUI.PropertyField(position, property, label, true);
//         GUI.enabled = true;
//     }
// }