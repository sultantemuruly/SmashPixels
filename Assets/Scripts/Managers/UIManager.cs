using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject dailyPanel;
    [SerializeField] private GameObject shopPanel;

    [Header("Buttons")]
    [SerializeField] private Button nextButton;
    [SerializeField] private Button[] settingsButton;
    [SerializeField] private Button[] dailyButton;
    [SerializeField] private Button[] shopButton;
    [SerializeField] private Button baseButton;

    // setting buttons
    [SerializeField] private Button soundButton;
    [SerializeField] private Button vibroButton;

    [Header("Images")]
    [SerializeField] private Image progressBar;
    public static Image ProgressBar { get; set; }

    [Header("Prefabs")]
    [SerializeField] private GameObject textEffectPrefab;

    [Header("Settings")]
    public int soundValue;
    public GameObject soundTick;
    public int vibroValue;
    public GameObject vibroTick;

    //others
    private float textXpos = 150f;

    private void OnEnable()
    {
        EventsManager.onStarted += OnStart;
        EventsManager.onGameEnded += OnInitialUI;
        EventsManager.onLevelCompleted += OnLevelCompletedUI;
        EventsManager.onTextEffect += OnTextEffect;
    }

    private void OnDisable()
    {
        EventsManager.onStarted -= OnStart;
        EventsManager.onGameEnded -= OnInitialUI;
        EventsManager.onLevelCompleted -= OnLevelCompletedUI;
        EventsManager.onTextEffect -= OnTextEffect;
    }

    private void Awake()
    {
        SetSettings();

        ProgressBar = progressBar;

        nextButton.onClick.AddListener(OnNextButton);

        for(int i=0; i<=1; i++)
        {
            settingsButton[i].onClick.AddListener(OnSettingsButton);
            dailyButton[i].onClick.AddListener(OnDailyButton);
            shopButton[i].onClick.AddListener(OnShopButton);
        }

        baseButton.onClick.AddListener(OnBaseButton);
        soundButton.onClick.AddListener(OnSoundButton);
        vibroButton.onClick.AddListener(OnVibroButton);
    }

    private void OnStart()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    private void OnLevelCompletedUI()
    {
        gamePanel.SetActive(false);
        finishPanel.SetActive(true);
    }

    private void OnInitialUI()
    {
        StartCoroutine(SwitchUI());
    }

    IEnumerator SwitchUI()
    {
        yield return new WaitForSeconds(1.5f);

        if(!GameManager.IsLevelCompleted)
        {
            startPanel.SetActive(true);
            gamePanel.SetActive(false);
        }
    }

    private void OnNextButton()
    {
        EventsManager.onGetMoney.Invoke(LevelsManager.MoneyReward);
        SceneManager.LoadScene(0);
    }

    private void OnSettingsButton()
    {
        if(settingsPanel.activeSelf) settingsPanel.SetActive(false);
        else settingsPanel.SetActive(true);
    }

    private void OnDailyButton()
    {
        if(dailyPanel.activeSelf) dailyPanel.SetActive(false);
        else dailyPanel.SetActive(true);
    }

    private void OnShopButton()
    {
        if(shopPanel.activeSelf) shopPanel.SetActive(false);
        else shopPanel.SetActive(true);
    }

    private void OnBaseButton()
    {
        SceneManager.LoadScene(1);
    }

    private void OnTextEffect(int count)
    {
        GameObject textEffectObj = Instantiate(textEffectPrefab, gamePanel.transform);
        textEffectObj.GetComponent<TextEffect>().moneyCount = count;

        textEffectObj.GetComponent<RectTransform>().localPosition = new Vector3(textXpos, 0f, 0f);
        textXpos *= -1;

        Destroy(textEffectObj, 1f);
    }

    private void SetSettings()
    {
        soundValue = PlayerPrefs.GetInt("SoundValue", 1);
        vibroValue = PlayerPrefs.GetInt("VibroValue", 1);

        if(soundValue==1)
        {
            soundTick.SetActive(true);
            AudioListener.volume = 1f;
        }
        else
        {
            soundTick.SetActive(false);
            AudioListener.volume = 0f;
        }

        if(vibroValue==1)
        {
            vibroTick.SetActive(true);
            GameManager.IsVibro = true;
        }
        else
        {
            vibroTick.SetActive(false);
            GameManager.IsVibro = false;
        }
    }

    private void OnSoundButton()
    {
        if(soundValue==1)
        {
            soundValue = 0;
            PlayerPrefs.SetInt("SoundValue", soundValue);

            soundTick.SetActive(false);
            AudioListener.volume = 0f;
        }
        else
        {
            soundValue = 1;
            PlayerPrefs.SetInt("SoundValue", soundValue);

            soundTick.SetActive(true);
            AudioListener.volume = 1f;
        }
    }

    private void OnVibroButton()
    {
        if(vibroValue==1)
        {
            vibroValue = 0;
            PlayerPrefs.SetInt("VibroValue", vibroValue);

            vibroTick.SetActive(false);
            GameManager.IsVibro = false;
        }
        else
        {
            vibroValue = 1;
            PlayerPrefs.SetInt("VibroValue", vibroValue);

            vibroTick.SetActive(true);
            GameManager.IsVibro = true;
        }
    }
}
