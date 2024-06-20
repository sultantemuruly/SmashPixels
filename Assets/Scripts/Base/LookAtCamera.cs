using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera targetCamera;

    private void Start()
    {
        targetCamera = Camera.main;
    }

    private void Update()
    {
        if (targetCamera != null)
        {
            transform.LookAt(targetCamera.transform);
        }
    }
}
