using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    public static Rigidbody rb { get; set; }
    private static Transform ballTransform;

    [SerializeField] private GameObject ballCopyPrefab;
    [SerializeField] private bool isPlayerBall;

    [SerializeField] private GameObject coinPrefab;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        ballTransform = BallPlayer.playerBallTransform;
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Pixel") || col.gameObject.CompareTag("BonusPixel"))
        {
            EventsManager.onPixelEffect.Invoke();
            EventsManager.onItemDestroyed.Invoke(col.gameObject, 0f, col.gameObject.GetComponent<Item>().itemId);

            if(col.gameObject.CompareTag("BonusPixel"))
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }

            return;
        }

        if(col.gameObject.CompareTag("Laser"))
        {
            Item item = col.gameObject.GetComponent<Item>();
            EventsManager.onElementActivated.Invoke(item.itemId);
            EventsManager.onItemDestroyed.Invoke(col.gameObject, 0.25f, item.itemId);

            return;
        }

        if(col.gameObject.CompareTag("Bomb"))
        {
            Item item = col.gameObject.GetComponent<Item>();
            EventsManager.onElementActivated.Invoke(item.itemId);
            EventsManager.onItemDestroyed.Invoke(col.gameObject, 0f, item.itemId);

            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BallCopy"))
        {
            Item item = other.GetComponent<Item>();
            EventsManager.onElementActivated.Invoke(item.itemId);
            EventsManager.setNewBall.Invoke(ballTransform, rb);
            EventsManager.onItemDestroyed.Invoke(other.gameObject, 0f, item.itemId);

            return;
        }

        if(other.CompareTag("Rocket"))
        {
            Item item = other.GetComponent<Item>();
            EventsManager.onElementActivated.Invoke(item.itemId);
            EventsManager.onItemDestroyed.Invoke(other.gameObject, 5f, item.itemId);

            return;
        }

        if(other.CompareTag("Money"))
        {
            if(!other.GetComponent<Coin>().isPickable)
            {
                other.GetComponent<Coin>().isPickable = true;
                return;
            }

            int rand = UnityEngine.Random.Range(10, 100);
            EventsManager.onGetMoney.Invoke(rand);
            EventsManager.onTextEffect.Invoke(rand);

            SoundManager.OnCashSound();
            Destroy(other.gameObject);
        } 

        if(other.CompareTag("Border") && isPlayerBall)
        {
            EventsManager.onGameEnded.Invoke();
            Destroy(gameObject, 1f);

            return;
        }
    }
}
