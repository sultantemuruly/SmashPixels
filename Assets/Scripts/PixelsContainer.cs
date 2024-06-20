using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelsContainer : MonoBehaviour
{
    [SerializeField] private int pixelsCount;
    private UIManager uiManager;

    private void OnEnable()
    {
        EventsManager.onPixelEffect += SetProgressBar;
        EventsManager.onGameEnded += CheckPixelsCount;
    }

    private void OnDisable()
    {
        EventsManager.onPixelEffect -= SetProgressBar;
        EventsManager.onGameEnded -= CheckPixelsCount;
    }

    private void CheckPixelsCount()
    {
        if(transform.childCount == 0)
        {
            GameManager.IsLevelCompleted = true;
            EventsManager.onLevelCompleted.Invoke();
        }
    }

    private void SetProgressBar()
    {
        UIManager.ProgressBar.fillAmount = 1f - (float)transform.childCount/pixelsCount;
        Debug.Log(1f - (float)transform.childCount/pixelsCount);
    }
}
