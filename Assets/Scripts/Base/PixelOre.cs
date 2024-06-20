using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PixelOre : MonoBehaviour
{
    [SerializeField] private int maxCount;
    private int progressCount;

    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject pixelPrefab;

    public int pixelOreId { get; set; }

    private void OnEnable()
    {
        EventsManager.onOreGetsHit += OnGetsHit;
    }

    private void OnDisable()
    {
        EventsManager.onOreGetsHit -= OnGetsHit;
    }

    private void Awake()
    {
        progressCount = maxCount;
        pixelOreId = gameObject.GetInstanceID();
    }

    private void OnGetsHit(int id)
    {
        if(pixelOreId != id) return;

        progressCount -= 1;
        progressBar.fillAmount = (float) progressCount/maxCount;

        if(progressCount <= 0 )
        {
            for(int i=0; i<5; i++)
            {
                Vector3 randomPosition = new Vector3( transform.position.x + Random.Range(-2f, 2f), 
                transform.position.y + Random.Range(0f, 2f), 
                transform.position.x + Random.Range(-2f, 2f));

                Vector3 randomRotation = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));

                Instantiate(pixelPrefab, randomPosition, Quaternion.Euler(randomRotation));
                Destroy(gameObject);
            }
        }
    }
}
