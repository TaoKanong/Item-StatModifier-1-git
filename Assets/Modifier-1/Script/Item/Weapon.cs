using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum WeaponType
{
    Primary,
    Secondary
}

[System.Serializable]
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
    public Sprite icon;

}

[System.Serializable]
public class WeaponStatDefinition
{
    public StatDefinition stat;
    public string statName => stat.name;
    public float magnitude;
}


[System.Serializable]
[CreateAssetMenu(menuName = "StatSystem/WeaponTest", fileName = "WeaponTest", order = 0)]
public class WeaponTest : ModuleMod
{
    public GameObject bulletPrefab;
    public GameObject hitPrefab;
    public GameObject flashPrefab;
    public GameObject soundEffect;
    public float ammoConsume;
    public WeaponType weaponType;
}
