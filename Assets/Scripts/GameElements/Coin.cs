using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector;
    public bool isPickable {get; set;}

    private void Update()
    {
        transform.Rotate(rotationVector * Time.deltaTime);
    }
}
