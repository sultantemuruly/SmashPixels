using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    public int moneyCount { get; set; }

    private void Start()
    {
        moneyText.text = "+" + moneyCount.ToString();
    }
}
