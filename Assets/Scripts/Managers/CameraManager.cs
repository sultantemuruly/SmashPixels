using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private float initialFOV;
    private static Camera cam;

    private static Transform target;
    public static void SetTarget(Transform val) { target = val; }

    [SerializeField] private float smoothSpeed = 0.125f;
    private Vector3 offset;
    private Vector3 initialPos;

    private void OnEnable()
    {
        EventsManager.onStarted += OnStart;
        EventsManager.onGameEnded += MoveToInitialPos;
    }

    private void OnDisable()
    {
        EventsManager.onStarted -= OnStart;
        EventsManager.onGameEnded -= MoveToInitialPos;
    }

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        offset = transform.position - target.position;
        initialPos = transform.position;
        initialFOV = cam.fieldOfView;
    }

    private void OnStart()
    {
        StartCoroutine(SwitchFOVSmoothly(75f));
    }

    private void LateUpdate()
    {
        if (target != null && GameManager.IsGameStarted)
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothSpeed);
        }
    }

    private void MoveToInitialPos()
    {
        StartCoroutine(SwitchPosSmoothly());
        StartCoroutine(SwitchFOVSmoothly(initialFOV));
    }

    private IEnumerator SwitchFOVSmoothly(float targetFOV)
    {
        GameManager.IsCameraTransitionCompleted = false;

        float t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime / 1.5f;

            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, t);
            yield return null;
        }

        cam.fieldOfView = targetFOV;
        GameManager.IsCameraTransitionCompleted = true;
    }

    private IEnumerator SwitchPosSmoothly()
    {
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime / 1.5f;
            transform.position = Vector3.Lerp(transform.position, initialPos, t);
            yield return null;
        }

        transform.position = initialPos;
    }
}
