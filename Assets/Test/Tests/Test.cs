using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class Test : MonoBehaviour
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        EditorSceneManager.LoadSceneInPlayMode("E:/Unity Works/StatModifier-1/Assets/Test/Scene/Test.unity", new UnityEngine.SceneManagement.LoadSceneParameters(UnityEngine.SceneManagement.LoadSceneMode.Single));
    }

    [UnityTest]
    public IEnumerator Stat_WheneModifierApply_ChangeValue()
    {
        yield return null;
        StatController statController = GameObject.FindObjectOfType<StatController>();
        // ModuleModController moduleController = GameObject.FindObjectOfType<ModuleModController>();
        Stat physicalAttack = statController.stats["PhysicalAttack"];
        Stat magicDefense = statController.stats["MagicDefense"];
        // Assert.AreEqual(0, physicalAttack.value);

        // StatModifier tempMod = new StatModifier // ใช้ได้ทุก Stat ไม่ต้องพิมพฺเพิ่ม  => ต้องมีหลายตัว เพราะถ้ามีตัวเดียว ทุก Stat ก็จะเป็นค่าเดียวกันหมด Magnitude แต่ละ Stat ต้องต่างกัน และ source randomGameObject ทุก stat ต้องเหมือนกัน
        // {
        //     source = RandomGameObject(),
        //     magnitude = 5f,
        //     Type = ModifierOperationType.Additive
        // };

        // foreach (KeyValuePair<string, Stat> pair in statController.stats) // ใช้ใน ModuleMod ตัว Module mod จะให้ใส่ Stat scriptableObject ลงไป อยากเพิ่มตัวไหนก็ใส่ลงไป 
        // {
        //     string key = pair.Key;
        //     Stat stat = statController.stats[key];
        //     stat.AddModifier(tempMod);
        // }

        // physicalAttack.AddModifier(tempMod);

        statController.EquipModule();
        Assert.AreEqual(25f, physicalAttack.value);
        // Assert.AreEqual(10, magicDefense.value);
        // physicalAttack.AddModifier(new StatModifier
        // {
        //     source = RandomGameObject(),
        //     magnitude = 5f,
        //     Type = ModifierOperationType.Additive
        // });

        // physicalAttack.RemoveModifierFromSource(tempMod.source);
        // Assert.AreEqual(5f, physicalAttack.value); // Assert.AreEqual(expect value, actual value)
    }
}
