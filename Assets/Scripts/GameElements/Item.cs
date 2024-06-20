using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int itemId { get; set; }
    public string SaveString { get; set; }
    private int saveValue;

    private void OnEnable()
    {
        EventsManager.onItemDestroyed += ItemDestroyed;
    }

    private void OnDisable()
    {
        EventsManager.onItemDestroyed -= ItemDestroyed;
    }

    private void Awake()
    {
        saveValue = PlayerPrefs.GetInt(SaveString);
        if(saveValue == 1) Destroy(gameObject);

        itemId = gameObject.GetInstanceID();
    }

    private void ItemDestroyed(GameObject gameObject, float destroyDelay, int id)
    {
        if(itemId != id) return;

        ItemsSavingManager.SaveStrings.Add(SaveString);

        Destroy(gameObject, destroyDelay);
    }
}
