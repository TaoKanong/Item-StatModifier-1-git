using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Stat
{
    protected StatDefinition m_statDefinition;
    protected float m_value;
    public float value => m_value;
    public virtual float baseValue => m_statDefinition.baseValue;
    public event Action valueChange;
    protected List<StatModifier> statModifiers = new List<StatModifier>();

    public Stat(StatDefinition statDefinition)
    {
        m_statDefinition = statDefinition;
        CalculateValue();
    }

    public void AddModifier(StatModifier statModifier)
    {
        statModifiers.Add(statModifier);
        // foreach (StatModifier mod in statModifiers)
        // {
        //     Debug.Log(mod.source);
        // }
        // Debug.Log(statModifiers.Count);
        CalculateValue();
    }

    public void RemoveModifierFromSource(Object source)
    {
        statModifiers = statModifiers.Where(x => x.source.GetInstanceID() != source.GetInstanceID()).ToList();
        // Debug.Log(statModifiers.Count);
        CalculateValue();
    }

    public void RemoveModifierByID(int id)
    {
        statModifiers = statModifiers.Where(x => x.id != id).ToList();
        CalculateValue();
    }

    public void ClearModifier()
    {
        statModifiers.Clear();
    }

    protected void CalculateValue()
    {
        float newValue = baseValue;
        statModifiers.Sort((x, y) => x.Type.CompareTo(y.Type));

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier modifier = statModifiers[i];

            if (modifier.Type == ModifierOperationType.Additive)
            {
                newValue += modifier.magnitude;
            }

            else if (modifier.Type == ModifierOperationType.Multiplicative)
            {
                newValue *= modifier.magnitude;
            }
        }

        // if (m_statDefinition.cap >= 0)
        // {
        //     newValue = Mathf.Min(newValue, m_statDefinition.cap);  // set maximum ของค่า finalValue = Mathf.Min(10, 5); ก็จะเป็น 5 เอาตัวที่น้อยที่สุด
        // }

        if (m_value != newValue)
        {
            m_value = newValue;
            valueChange?.Invoke(); // ? เช็คว่าเป็น null รึป่าว
        }
    }
}
