using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;

    private Rigidbody rb;
    private Item item;

    [SerializeField] private GameObject coinPrefab;

    private bool isActivated;

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
        rb = GetComponent<Rigidbody>();
        item = GetComponent<Item>();
    }

    private void OnActivated(int id)
    {
        if(item.itemId != id) return;

        isActivated = true;
        rb.velocity = direction * speed;
        SoundManager.OnRocketSound();
    }

    private void OnTriggerEnter(Collider other)
    {
        if( (other.CompareTag("Pixel") || other.CompareTag("BonusPixel") ) && isActivated)
        {
            EventsManager.onPixelEffect.Invoke();
            EventsManager.onItemDestroyed.Invoke(other.gameObject, 0f, other.GetComponent<Item>().itemId);

            if(other.CompareTag("BonusPixel"))
            {
                GameObject newCoin = Instantiate(coinPrefab, other.transform.position, Quaternion.identity);
                newCoin.GetComponent<Coin>().isPickable = true;
            }

            return;
        }
    }
}
