using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsSavingManager : MonoBehaviour
{
    [SerializeField] private Item[] items;

    private static List<string> saveStrings = new List<string>();

    public static List<string> SaveStrings
    {
        get { return saveStrings; }
        set { saveStrings = value; }
    }

    [SerializeField] private int bonusPixelsCount;
    [SerializeField] private int randomStart;
    [SerializeField] private List<int> randomValues  = new List<int>();

    private void OnEnable()
    {
        EventsManager.onGameEnded += SaveDestroyedItems;
        EventsManager.onLevelCompleted += ResetItemsSavings;
    }

    private void OnDisable()
    {
        EventsManager.onGameEnded -= SaveDestroyedItems;
        EventsManager.onLevelCompleted -= ResetItemsSavings;
    }

    private void Awake()
    {
        for(int i=0; i<bonusPixelsCount; i++)
        {
            int rand = Random.Range(randomStart, items.Length);

            if(randomValues.Contains(rand)) i--;
            else 
            {
                items[rand].gameObject.tag = "BonusPixel";
                randomValues.Add(rand);
            }
        }

        for(int i=0; i<items.Length; i++)
        {
            items[i].SaveString = "item" + i.ToString();
        }
    }

    private void SaveDestroyedItems()
    {
        foreach (string str in saveStrings)
        {
            PlayerPrefs.SetInt(str, 1);
        }
    }

    private void ResetItemsSavings()
    {
        for(int i=0; i<items.Length; i++)
        {
            PlayerPrefs.SetInt("item" + i.ToString(), 0);
        }
    }
}
