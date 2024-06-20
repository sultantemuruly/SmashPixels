using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSManager : MonoBehaviour
{
    [SerializeField] private int targetFPS;

    private void Start()
    {
        //QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;
    }

    private void Update()
    {
        if(Application.targetFrameRate != targetFPS)
            Application.targetFrameRate = targetFPS;
    }
}
