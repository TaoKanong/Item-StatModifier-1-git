using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AttributeTest
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        EditorSceneManager.LoadSceneInPlayMode("E:/Unity Works/StatModifier-1/Assets/Test/Scene/Test.unity", new UnityEngine.SceneManagement.LoadSceneParameters(UnityEngine.SceneManagement.LoadSceneMode.Single));
    }

    [UnityTest]
    public IEnumerator Attribute_WheneModifierApply_DoesNotExceedMaximumValue()
    {
        yield return null;
        StatController statController = GameObject.FindObjectOfType<StatController>();
        Attribute health = statController.stats["Health"] as Attribute;
        Assert.AreEqual(100, health.value);
        Assert.AreEqual(100, health.currentValue);
        health.ApplyModifier(new StatModifier
        {
            magnitude = 20f,
            Type = ModifierOperationType.Additive
        });
        Assert.AreEqual(100, health.currentValue);
    }

    [UnityTest]
    public IEnumerator Attribute_WheneModifierApply_DoesNotGoBelowZero()
    {
        yield return null;
        StatController statController = GameObject.FindObjectOfType<StatController>();
        Attribute health = statController.stats["Health"] as Attribute;
        Assert.AreEqual(100, health.value);
        Assert.AreEqual(100, health.currentValue);
        health.ApplyModifier(new StatModifier
        {
            magnitude = -120f,
            Type = ModifierOperationType.Additive
        });
        Assert.AreEqual(0, health.currentValue);
    }
}
