using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Primary,
    Secondary
}

[CreateAssetMenu(menuName = "StatSystem/Weapon", fileName = "Weapon", order = 0)]
public class Weapon : ScriptableObject
{
    public List<WeaponStatDefinition> statList;
    public GameObject bulletPrefab;
    public GameObject hitPrefab;
    public GameObject flashPrefab;
    public GameObject soundEffect;
    public float ammoConsume;
    public WeaponType weaponType;

}

[System.Serializable]
public class WeaponStatDefinition
{
    public StatDefinition stat;
    public string statName => stat.name;
    public float magnitude;
}
