using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPlayer : MonoBehaviour
{
    private Rigidbody rb;
    public static FloatingJoystick joystick;

    [SerializeField] private float moveSpeed;
    public static Transform playerBallTransform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerBallTransform = transform;
    }

    private void Start()
    {
        joystick = EventsManager.getJoystick.Invoke();
    }

    private void FixedUpdate()
    {
        if (GameManager.IsGameStarted)
        {
            rb.velocity = new Vector3(moveSpeed * Input.GetAxis("Horizontal") /*joystick.Horizontal*/ , rb.velocity.y, 0f);

            if(rb.velocity.y >= 8f)
            {
                rb.velocity = new Vector3(rb.velocity.x, 8f, 0f);
            }

            if(rb.rotation.eulerAngles.magnitude == 0f)
            {
                rb.AddTorque(new Vector3(0f, 0f, 1f), ForceMode.Impulse);
            }
        }
    }
}
