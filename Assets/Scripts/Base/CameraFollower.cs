using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Vector3 offset;

    private void Awake()
    {
        offset = transform.position - playerTransform.position;
    }

    private void Update()
    {
        transform.position = playerTransform.position + offset;
    }
}
