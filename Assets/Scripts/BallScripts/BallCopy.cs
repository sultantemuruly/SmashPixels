using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCopy : MonoBehaviour
{
    private Transform target;
    private float deltaOffset;

    private void OnEnable()
    {
        EventsManager.setNewBall += SetBallCopy;
    }

    private void OnDisable()
    {
        EventsManager.setNewBall -= SetBallCopy;
    }

    private void Start()
    {
        deltaOffset = transform.position.x - target.position.x;
    }

    private void Update()
    {
        if(target != null)
        {
            transform.position = new Vector3(target.position.x + deltaOffset, transform.position.y, transform.position.z);
        }
        else
        {
            Destroy(gameObject);   
        }
    }
    
    private void SetBallCopy(Transform targetTransform, Rigidbody rb)
    {
        target = targetTransform;
        GetComponent<Rigidbody>().velocity = rb.velocity;
    }
}
