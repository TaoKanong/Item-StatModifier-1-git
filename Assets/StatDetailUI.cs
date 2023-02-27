using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatDetailUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    private Dictionary<string, Stat> _stats;
    private Dictionary<string, Stat> _baseStats;
    bool isInitialized = false;
    private void Awake()
    {

    }
    void Start()
    {
        if (isInitialized == false)
        {
            Initialize();
        }
    }

    void Initialize()
    {
        _stats = StatController.Instance.stats;
        _baseStats = StatController.Instance.baseStats;

        InventorySystem.Instance.equipItem += UpdateStatDetailUI;
        InventorySystem.Instance.unEquipItem += UpdateStatDetailUI;

        isInitialized = true;
        RenderStatDetail();
    }

    void UpdateStatDetailUI()
    {
        RenderStatDetail();
    }

    void RenderStatDetail()
    {
        _text.text = "";
        foreach (KeyValuePair<string, Stat> pair in _stats)
        {
            float oldValue = 0;
            float finalValue = 0;

            CompareNewValueAndBaseValue(pair, ref oldValue, ref finalValue);
            RenderText(pair, oldValue, finalValue);
        }
    }

    private void RenderText(KeyValuePair<string, Stat> pair, float oldValue, float finalValue)
    {
        if (finalValue == 0)
        {
            _text.text += pair.Key + " " + _stats[pair.Key].value + "\n";
        }
        else
        {
            _text.text += $"{pair.Key} {_stats[pair.Key].value} <color=green> ({oldValue} + {finalValue}) </color=green> " + "\n";
        }
    }

    private void CompareNewValueAndBaseValue(KeyValuePair<string, Stat> pair, ref float oldValue, ref float finalValue)
    {
        foreach (KeyValuePair<string, Stat> basePair in _baseStats)
        {
            float newValue = _stats[pair.Key].value;
            float baseValue = _baseStats[basePair.Key].value;

            bool isDifferentValue = CheckSimilarKeyPair(pair.Key, basePair.Key) && CheckDifferentValuePair(newValue, baseValue);
            if (isDifferentValue)
            {
                finalValue = ChangeValue(baseValue, newValue);
                oldValue = baseValue;

                break;
            }
        }
    }

    bool CheckSimilarKeyPair(string firstName, string secondName)
    {
        if (firstName == secondName) return true;
        return false;
    }

    bool CheckDifferentValuePair(float firstValue, float secondValue)
    {
        if (firstValue != secondValue) return true;
        return false;
    }

    float ChangeValue(float baseValue, float newValue)
    {
        float changeValue = baseValue < newValue ? newValue - baseValue : baseValue - newValue;
        return changeValue;
    }
}
