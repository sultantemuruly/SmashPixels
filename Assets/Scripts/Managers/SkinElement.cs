using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinElement : MonoBehaviour
{
    private bool isUnlocked;
    [SerializeField] private int skinIndex;

    [SerializeField] private GameObject unlockedCell;
    [SerializeField] private GameObject selectedCell;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnSelected);
    }

    private void OnSelected()
    {
        if(isUnlocked)
        {
            EventsManager.onResetSkin.Invoke();
            EventsManager.onSetSkinIndex.Invoke(skinIndex);
            selectedCell.SetActive(true);
        }
    }

    public void OnUnlocked()
    {
        isUnlocked = true;
        unlockedCell.SetActive(true);
        selectedCell.SetActive(true);
    }

    public void OnDisselected()
    {
        selectedCell.SetActive(false);
    }
}
