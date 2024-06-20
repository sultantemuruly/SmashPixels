using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PixelArt : MonoBehaviour
{
    [SerializeField] private GameObject[] pixels;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject unlockObject;

    private int maxPixelCount;
    private int currentPixelCount;
    [SerializeField] private string saveString;

    public int pixelArtId { get; set; }

    private void OnEnable()
    {
        EventsManager.onFillWithPixels += FillWithPixels;
    }

    private void OnDisable()
    {
        EventsManager.onFillWithPixels -= FillWithPixels;
    }

    private void Awake()
    {
        maxPixelCount = pixels.Length;
        currentPixelCount = PlayerPrefs.GetInt(saveString);

        for(int i=0; i<currentPixelCount; i++)
        {
            pixels[i].SetActive(true);
        }

        SetProgress();

        if(currentPixelCount >= maxPixelCount)
        {
            unlockObject.SetActive(true);
            Destroy(gameObject);
        }

        pixelArtId = gameObject.GetInstanceID();
    }

    private void FillWithPixels(int id)
    {
        if(pixelArtId != id) return;

        currentPixelCount++;
        PlayerPrefs.SetInt(saveString, currentPixelCount);

        SetProgress();

        if(currentPixelCount >= maxPixelCount)
        {
            pixels[maxPixelCount-1].SetActive(true);
            unlockObject.SetActive(true);
            Destroy(gameObject);
        }
        else
        {
            pixels[0].SetActive(true);
            pixels[currentPixelCount].SetActive(true);   
        }
    }

    private void SetProgress()
    {
        float percentage = ((float) currentPixelCount/maxPixelCount) * 100f;
        percentage = (float)Math.Round(percentage, 1);

        progressText.text = percentage.ToString() + "%";
        progressBar.fillAmount = (float) currentPixelCount/maxPixelCount;
    }
}
