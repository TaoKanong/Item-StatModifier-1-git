using System.Collections;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using UnityEngine;

[assembly: InternalsVisibleTo("StatSystem.Tests")]
public class PrimaryStat : Stat//, ISavable
{
    private float m_BaseValue;
    public override float baseValue => m_BaseValue;

    public PrimaryStat(StatDefinition statDefinition) : base(statDefinition)
    {
        m_BaseValue = statDefinition.baseValue;
        CalculateValue();
    }

    internal void Add(float amount)
    {
        m_BaseValue += amount;
        CalculateValue();
    }

    internal void Subtract(float amount)
    {
        m_BaseValue -= amount;
        CalculateValue();
    }

    // # region Stat system

    // public object data => new PrimaryStatData
    // {
    //     baseValue = baseValue
    // };
    // public void Load(object data)
    // {
    //     PrimaryStatData primaryStatData = (PrimaryStatData)data;
    //     m_BaseValue = primaryStatData.baseValue;
    //     CalculateValue();
    // }

    // [Serializable]
    // protected class PrimaryStatData
    // {
    //     public float baseValue;
    // }

    // # endregion
}
