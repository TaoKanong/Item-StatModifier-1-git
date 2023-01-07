using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatSystem/StatDatabase", fileName = "StatDatabase", order = 0)]
public class StatDatabase : ScriptableObject
{
    public List<StatDefinition> stat;
    public List<StatDefinition> attributes;
    public List<StatDefinition> primaryStats;
}
