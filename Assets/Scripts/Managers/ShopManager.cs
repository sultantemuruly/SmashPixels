using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance { get; private set; }

    public static int currentIndex { get; set; }
    private const string CurrentIndexKey = "CurrentIndex";
    private List<int> lockedIndexes = new List<int>();

    private int price;
    private const string PriceKey = "Price";
    [SerializeField] private TextMeshProUGUI priceText;

    [SerializeField] private Button unlockButton;
    [SerializeField] private SkinElement[] skinElements;

    private void OnEnable()
    {
        EventsManager.onSetSkinIndex += OnSetIndex;
        EventsManager.onResetSkin += ResetSkin;
    }

    private void OnDisable()
    {
        EventsManager.onSetSkinIndex -= OnSetIndex;
        EventsManager.onResetSkin -= ResetSkin;
    }

    private void Awake()
    {
        currentIndex = PlayerPrefs.GetInt(CurrentIndexKey);
        price = PlayerPrefs.GetInt(PriceKey, 100);
        priceText.text = price.ToString();
        
        skinElements[0].OnUnlocked();
        if(currentIndex != 0) skinElements[0].OnDisselected();

        for(int i=1; i<9; i++)
        {
            if( PlayerPrefs.GetInt("Index" + i.ToString()) == 0 )
            {
                lockedIndexes.Add(i);
            }
            else
            {
                skinElements[i].OnUnlocked();

                if(currentIndex != i) skinElements[i].OnDisselected();
            }
        }

        unlockButton.onClick.AddListener(OnUnlockButton);
    }

    private void OnUnlockButton()
    {
        if(lockedIndexes.Count == 0) return;

        if(/*MoneyManager.MoneyCount >= price*/ true)
        {
            /*EventsManager.onReduceMoney.Invoke(price);

            price *= 2;
            priceText.text = price.ToString();
            PlayerPrefs.SetInt(PriceKey, price);*/

            skinElements[currentIndex].OnDisselected();

            int rand = Random.Range(0, lockedIndexes.Count);
            currentIndex = lockedIndexes[rand];
            PlayerPrefs.SetInt(CurrentIndexKey, currentIndex);
            lockedIndexes.RemoveAt(rand);
            PlayerPrefs.SetInt("Index" + rand.ToString(), 1);

            skinElements[currentIndex].OnUnlocked();
        } 
    }

    private void ResetSkin()
    {
        skinElements[currentIndex].OnDisselected();
    }

    private void OnSetIndex(int index)
    {
        currentIndex = index;
        PlayerPrefs.SetInt(CurrentIndexKey, currentIndex);
    }
}
