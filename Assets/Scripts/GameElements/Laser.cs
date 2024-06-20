using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private GameObject laserEffectPrefab;
    [SerializeField] List<Item> pixels = new List<Item>();
    private Item item;

    [SerializeField] private GameObject coinPrefab;

    private void OnEnable()
    {
        EventsManager.onElementActivated += OnActivated;
    }

    private void OnDisable()
    {
        EventsManager.onElementActivated -= OnActivated;
    }

    private void Awake()
    {
        item = GetComponent<Item>();
    }

    private void OnActivated(int id)
    {
        if(item.itemId != id) return;

        foreach (Item pixel in pixels)
        {
            if(pixel == null) continue;

            EventsManager.onItemDestroyed.Invoke(pixel.gameObject, 0f, pixel.itemId);

            if(pixel.gameObject.CompareTag("BonusPixel"))
            {
                GameObject newCoin = Instantiate(coinPrefab, pixel.transform.position, Quaternion.identity);
                newCoin.GetComponent<Coin>().isPickable = true;
            }
        }

        SoundManager.OnLaserSound();

        Destroy(Instantiate(laserEffectPrefab, transform.position, transform.rotation), 0.25f);
    }
}
