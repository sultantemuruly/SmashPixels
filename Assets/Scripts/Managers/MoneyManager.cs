using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static int MoneyCount { get; set; }
    [SerializeField] private TextMeshProUGUI moneyText;
    private const string MoneyCountKey = "MoneyCount";

    private void OnEnable()
    {
        EventsManager.onGetMoney += OnGetMoney;
        EventsManager.onReduceMoney += OnReduceMoney;
    }

    private void OnDisable()
    {
        EventsManager.onGetMoney -= OnGetMoney;
        EventsManager.onReduceMoney -= OnReduceMoney;
    }

    private void Awake()
    {
        MoneyCount = PlayerPrefs.GetInt(MoneyCountKey);
        moneyText.text = MoneyCount.ToString();
    }

    private void OnGetMoney(int addMoney)
    {
        MoneyCount += addMoney;
        moneyText.text = MoneyCount.ToString();
        PlayerPrefs.SetInt(MoneyCountKey, MoneyCount);
    }

    private void OnReduceMoney(int reduceMoney)
    {
        MoneyCount += reduceMoney;
        moneyText.text = MoneyCount.ToString();
        PlayerPrefs.SetInt(MoneyCountKey, MoneyCount);
    }
}
