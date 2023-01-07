using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatSystem/StatDefinition", fileName = "StatDefinition", order = 0)]
public class StatDefinition : ScriptableObject
{
    [SerializeField] private float m_BaseValue; //m_ คือ member variables scope ใช้ใน class จะอยู่ใช้งานตลอดไปกับ class 
    [SerializeField] private float m_Cap = -1;
    public float baseValue => m_BaseValue; // => คือ read only
    public float cap => m_Cap;
}
