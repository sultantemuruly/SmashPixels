using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DailyManager : MonoBehaviour
{
    [SerializeField] private GameObject[] claimedDays;
    [SerializeField] private Button claimButton;
    [SerializeField] private GameObject activeButton;
    [SerializeField] private bool isButtonActive;

    private TimeSpan nextRewardTime;
    private TimeSpan rewardInterval = new TimeSpan(0, 0, 15);
    private const string NextRewardTimeKey = "NextRewardTime";
    private const string LastExitTimeKey = "LastExitTime";
    [SerializeField] private TextMeshProUGUI nextRewardText;

    private int currentDay;
    private const string CurrentDayKey = "CurrentDay";

    [SerializeField] private int[] moneyReward;

    private void Awake()
    {
        currentDay = PlayerPrefs.GetInt(CurrentDayKey);

        if(currentDay >= 7)
        {
            nextRewardText.text = "All rewards are claimed!";
            for(int i=0; i<7; i++) claimedDays[i].SetActive(true);

            return;
        }

        for(int i=0; i<currentDay; i++) claimedDays[i].SetActive(true);

        claimButton.onClick.AddListener(OnClaimButton);

        // setting nextRewardTime saving
        if (PlayerPrefs.HasKey(NextRewardTimeKey))
        {
            nextRewardTime = TimeSpan.Parse(PlayerPrefs.GetString(NextRewardTimeKey));
        }
        else
        {
            nextRewardTime = rewardInterval;
            PlayerPrefs.SetString(NextRewardTimeKey, nextRewardTime.ToString());
        }

        // setting nextRewardTime with lastExitTime
        if (PlayerPrefs.HasKey(LastExitTimeKey))
        {
            DateTime lastExitTime = DateTime.Parse(PlayerPrefs.GetString(LastExitTimeKey));
            nextRewardTime -= DateTime.Now - lastExitTime;;

            if (nextRewardTime <= TimeSpan.Zero)
            {
                activeButton.SetActive(true);
                isButtonActive = true;
                nextRewardText.text = "Next Reward In: " + string.Format("{0:D2}:{1:D2}:{2:D2}", TimeSpan.Zero.Hours, TimeSpan.Zero.Minutes, TimeSpan.Zero.Seconds);
            }
        }

        // updating nextRewardTime
        InvokeRepeating(nameof(UpdateRewardTime), 1f, 1f);
    }

    private void UpdateRewardTime()
    {
        if(isButtonActive) return;

        nextRewardTime -= TimeSpan.FromSeconds(1);
        nextRewardText.text = "Next Reward In: " + string.Format("{0:D2}:{1:D2}:{2:D2}", nextRewardTime.Hours, nextRewardTime.Minutes, nextRewardTime.Seconds);
        
        if (nextRewardTime <= TimeSpan.Zero)
        {
            activeButton.SetActive(true);
            isButtonActive = true;
        }
    }

    private void OnApplicationQuit()
    {
        if(currentDay >= 7) return;
        
        PlayerPrefs.SetString(NextRewardTimeKey, nextRewardTime.ToString());
        PlayerPrefs.SetString(LastExitTimeKey, DateTime.Now.ToString());
    }

    private void OnClaimButton()
    {
        if(!isButtonActive) return;

        claimedDays[currentDay].SetActive(true);
        EventsManager.onGetMoney.Invoke(moneyReward[currentDay]);

        currentDay++;
        PlayerPrefs.SetInt(CurrentDayKey, currentDay);

        isButtonActive = false;
        activeButton.SetActive(false);
        nextRewardTime = rewardInterval;
        PlayerPrefs.SetString(NextRewardTimeKey, nextRewardTime.ToString());
    }
}
