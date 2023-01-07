using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PrimaryStatTest
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        EditorSceneManager.LoadSceneInPlayMode("E:/Unity Works/StatModifier-1/Assets/Test/Scene/Test.unity", new UnityEngine.SceneManagement.LoadSceneParameters(UnityEngine.SceneManagement.LoadSceneMode.Single));
    }

    [UnityTest]
    public IEnumerator Stat_WhenAddCall_ChangeBaseValue()
    {
        yield return null;
        StatController statController = GameObject.FindObjectOfType<StatController>();
        PrimaryStat strength = statController.stats["Strength"] as PrimaryStat;
        Assert.AreEqual(1, strength.value);
        strength.Add(1);
        Assert.AreEqual(2, strength.value);
    }
}
