using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "ScriptableObjects/LevelInfo", order = 1)]
public class LevelsInfo : ScriptableObject
{
    [SerializeField] private GameObject[] levels;
    [SerializeField] private int[] moneyRewards;

    public GameObject[] Levels => levels;
    public int[] MoneyRewards => moneyRewards;
}
