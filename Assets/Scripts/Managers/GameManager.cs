using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{   
    public static bool IsGameStarted { get; set; }
    public static bool IsLevelCompleted { get; set; }

    public static bool IsCameraTransitionCompleted { get; set; }
    public static bool IsVibro { get; set; }

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Vector3 ballPosition;
    [SerializeField] private FloatingJoystick joystick;
    private Rigidbody ballRb;

    private void OnEnable()
    {
        EventsManager.onGameEnded += OnGameEnded;
        EventsManager.getJoystick += OnSetJoystick;
    }

    private void OnDisable()
    {
        EventsManager.onGameEnded -= OnGameEnded;
        EventsManager.getJoystick -= OnSetJoystick;
    }

    private void Awake()
    {
        GameObject newBall = Instantiate(ballPrefab, ballPosition, Quaternion.identity);
        ballRb = newBall.GetComponent<Rigidbody>();
        CameraManager.SetTarget(newBall.transform);

        IsCameraTransitionCompleted = true;
    }

    public void StartGame()
    {
        if(!IsCameraTransitionCompleted) return;

        IsGameStarted = true;
        ballRb.useGravity = true;
        EventsManager.onStarted.Invoke();
    }

    private void OnGameEnded()
    {
        IsGameStarted = false;
        GameObject newBall = Instantiate(ballPrefab, ballPosition, Quaternion.identity);
        ballRb = newBall.GetComponent<Rigidbody>();
        CameraManager.SetTarget(newBall.transform);
    }

    private FloatingJoystick OnSetJoystick()
    {
        return joystick;
    }
}
