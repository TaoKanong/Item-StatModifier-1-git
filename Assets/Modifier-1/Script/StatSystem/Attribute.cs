using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute : Stat//, ISavable
{
    protected float m_CurrentValue;
    public float currentValue => m_CurrentValue;
    public event Action currentValueChange;
    public event Action<StatModifier> appliedModifier;

    public Attribute(StatDefinition statDefinition) : base(statDefinition)
    {
        m_CurrentValue = value;
    }

    public virtual void ApplyModifier(StatModifier modifier)
    {
        float newValue = m_CurrentValue;
        switch (modifier.Type)
        {
            case ModifierOperationType.Override:
                newValue = modifier.magnitude;
                break;
            case ModifierOperationType.Additive:
                newValue += modifier.magnitude;
                break;
            case ModifierOperationType.Multiplicative:
                newValue *= modifier.magnitude;
                break;
        }
        newValue = Mathf.Clamp(newValue, 0, m_value);

        if (newValue != m_CurrentValue)
        {
            m_CurrentValue = newValue;
            currentValueChange?.Invoke();
            appliedModifier?.Invoke(modifier);
        }
    }

    // # region Save system

    // public object data => new AttributeData
    // {
    //     currentValue = currentValue
    // };
    // public void Load(object data)
    // {
    //     AttributeData attributeData = (AttributeData)data;
    //     m_CurrentValue = attributeData.currentValue;
    //     currentValueChange?.Invoke();
    // }

    // [Serializable]
    // protected class AttributeData
    // {
    //     public float currentValue;
    // }

    // #endregion

}
