using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModifierOperationType
{
    Additive,
    Multiplicative,
    Override
}

public class StatModifier
{
    public Object source { get; set; }
    public float magnitude { get; set; }
    public ModifierOperationType Type { get; set; }
}
