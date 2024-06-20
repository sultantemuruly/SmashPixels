using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionRadius;
    private Item item;

    [SerializeField] private GameObject coinPrefab;

    private void OnEnable()
    {
        EventsManager.onElementActivated += OnExploded;
    }

    private void OnDisable()
    {
        EventsManager.onElementActivated -= OnExploded;
    }

    private void Awake()
    {
        item = GetComponent<Item>();
    }

    private void OnExploded(int id)
    {
        if(item.itemId != id) return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in colliders)
        {
            if(hit.gameObject.CompareTag("Rocket"))
            {
                continue;
            }

            if(hit.gameObject.CompareTag("Pixel") || hit.gameObject.CompareTag("BonusPixel"))  
            {
                hit.gameObject.AddComponent<Rigidbody>();
                hit.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                EventsManager.onItemDestroyed.Invoke(hit.gameObject, 1f, hit.gameObject.GetComponent<Item>().itemId);
                EventsManager.onPixelEffect.Invoke();

                if(hit.gameObject.CompareTag("BonusPixel"))
                {
                    GameObject newCoin = Instantiate(coinPrefab, hit.transform.position, Quaternion.identity);
                    newCoin.GetComponent<Coin>().isPickable = true;
                }
            }

            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius, 3.0F);
            }
        }

        SoundManager.OnBombSound();
    }
}
