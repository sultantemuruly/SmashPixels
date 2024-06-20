using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCopySpawner : MonoBehaviour
{
    [SerializeField] private Vector3 ballCopyOffset;
    [SerializeField] private GameObject ballCopyPrefab;
    private Item item;

    private void OnEnable()
    {
        EventsManager.onElementActivated += SpawnBall;
    }

    private void OnDisable()
    {
        EventsManager.onElementActivated -= SpawnBall;
    }

    private void Awake()
    {
        item = GetComponent<Item>();
    }

    private void SpawnBall(int id)
    {
        if(item.itemId != id) return;
        
        Instantiate(ballCopyPrefab, ballCopyOffset, Quaternion.identity);
    } 
}
