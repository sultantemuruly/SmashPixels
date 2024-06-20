using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelsManager : MonoBehaviour
{
    private int currentLevel;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private LevelsInfo levelsInfo;

    public static int MoneyReward;

    private void OnEnable()
    {
        EventsManager.onLevelCompleted += OnUpdateLevel;
    }

    private void OnDisable()
    {
        EventsManager.onLevelCompleted -= OnUpdateLevel;
    }

    private void Awake()
    {
        MoneyReward = levelsInfo.MoneyRewards[currentLevel];
        currentLevel = PlayerPrefs.GetInt("Level");

        if(currentLevel >= levelsInfo.Levels.Length)
        {
            currentLevel = 0;
        }

        levelText.text = "Level " + (currentLevel+1).ToString();
        Instantiate(levelsInfo.Levels[currentLevel], transform.position, Quaternion.identity);
    }

    private void OnUpdateLevel()
    {
        currentLevel += 1;
        PlayerPrefs.SetInt("Level", currentLevel);
    }
}
