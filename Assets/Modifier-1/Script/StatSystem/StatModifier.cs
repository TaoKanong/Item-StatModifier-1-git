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
    public int id { get; set; }
    public int weaponId { get; set; }
    public Object source { get; set; }
    public float magnitude { get; set; }
    public ModifierOperationType Type { get; set; }
}
