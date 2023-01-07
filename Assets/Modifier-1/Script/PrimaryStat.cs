using System.Collections;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using UnityEngine;

[assembly: InternalsVisibleTo("StatSystem.Tests")]
public class PrimaryStat : Stat
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
}
