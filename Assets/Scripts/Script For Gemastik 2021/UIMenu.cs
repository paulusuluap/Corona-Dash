using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    [Header("Texts")]
    public TextMeshProUGUI walletText;
    public TextMeshProUGUI selectedLanguageText = default;
    public TextMeshProUGUI[] highScoreTexts = new TextMeshProUGUI[5]; //Nanti dicari metode lain

    [Header("Rect Transforms")]
    public RectTransform mainMenu;
    public RectTransform settingsMenu;
    public RectTransform powerUpsMenu;
    public RectTransform fade;

    [Header("Languages Button")]
    public Button selectedLanguage;
    public Button bahasa;
    public Button english;

    [Header("Music Buttons & Others")]
    public Button exitSetting_Button;
    public Button msc_Button;
    public Button sou_Button;
    public Button vib_Button;
    public Image[] buttonImages = new Image[3];

    private float myMoney, initMoney, addedMoney; //initMoney tempat store myMoney awal awake
    private float textUpdateTime = 1.5f;
    private bool isMoneyUpdated = false;
    private bool isHidden = false; // for settings UI
    public Transform[] bottomTransform = new Transform[2]; 
    public Transform tapOnPlayTransform; //character hanya percobaan satu stage
    public CanvasGroup settingsGroup;
    
    private void Awake() {
        //Tween Controller
        DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(2000, 100);

        //Load HighScore
        for(int i = 0 ; i < SceneManagerScript.instance.numberOfWorlds ; i++)
        highScoreTexts[i].text = PlayerPrefs.GetInt("Highscore_"+(i+1).ToString()).ToString();
    }

    private void Start() {

        myMoney = SaveManager.Instance.playerMoney;
        addedMoney = SaveManager.Instance.gainedMoney;
        initMoney = myMoney;

        SaveManager.Instance.playerMoney = Mathf.RoundToInt(myMoney + addedMoney);
        SaveManager.Instance.gainedMoney = 0;
        SaveManager.Instance.Save();
        
        if(addedMoney > 0) StartCoroutine(Pulse());

        //Tap To Play Yoyo Effect (bottomTranform are TapOnPlay and Buy Button)
        YoyoScalingUpDown();
        
        //Audio Button Images On Start Menu
        AudioButtonImageMenu();
    }

    private void Update() {
        if(!isMoneyUpdated) 
        UpdatingWallet();

        UnlockedWorldsChecker();
    }

    //Updating Wallet Smooth with Lerp
    private void UpdatingWallet()
    {
        if(SaveManager.Instance.playerMoney < Mathf.Round(initMoney))
        {
            myMoney = Mathf.Lerp(myMoney, SaveManager.Instance.playerMoney, textUpdateTime * Time.deltaTime);
            walletText.text = (myMoney).ToString("0");
        }

        if(addedMoney == 0) 
        {
            walletText.text = (myMoney).ToString("0");
            return;
        }
        else
        {   
            initMoney = Mathf.Lerp(initMoney, Mathf.RoundToInt(myMoney + addedMoney), textUpdateTime * Time.deltaTime);
            walletText.text = Mathf.RoundToInt(initMoney).ToString("0");

            if(Mathf.RoundToInt(initMoney) == Mathf.RoundToInt(myMoney + addedMoney))
            {
                isMoneyUpdated = true;
                return;
            }
        }
    }

    //Yoyo effect for play & buy image
    private void YoyoScalingUpDown()
    {
        ShopSystem.Instance.play.transform.DOScale(new Vector3(18.5f, 18.5f, 1f), 1.5f)
        .SetEase(Ease.InOutSine)
        .SetLoops(-1, LoopType.Yoyo);

        ShopSystem.Instance.buy.transform.DOScale(new Vector3(0.925f, 0.925f, 1f), 1.5f)
        .SetEase(Ease.InOutSine)
        .SetLoops(-1, LoopType.Yoyo);
    }

    //Stage Selection
    public void FadeSceneTransition (int index)
    {
        AudioManager.PlaySound("PlayButton");
        PlayerPrefs.SetInt("FromMenu", 1);
        
        //Change DOScale below with black transition
        fade.DOAnchorPos(new Vector2(0f, 0f), 1.5f).SetEase(Ease.InSine)
            .OnComplete(()=> SceneManagerScript.instance.LoadWorld(index));
    }

    // Settings
    public void SettingsMenu()
    {
        AudioManager.PlaySound("Button");
        mainMenu.DOAnchorPos(new Vector2(0f, 2300f), 0.25f);
        settingsMenu.DOAnchorPos(new Vector2(0f, 0f), 0.25f).SetDelay(0.25f);
    }
    public void ExitSettings()
    {
        AudioManager.PlaySound("Button");
        settingsMenu.DOAnchorPos(new Vector2(-1100f, 0f), 0.25f);
        mainMenu.DOAnchorPos(new Vector2(0f, 0f), 0.25f).SetDelay(0.25f);
    }
    private void InteractableControl(bool state)
    {
        exitSetting_Button.interactable = state;
        msc_Button.interactable = state;
        sou_Button.interactable = state;
        vib_Button.interactable = state;
        
    }

    public void SelectLanguage()
    {
        InteractableControl(false);
        isHidden = true;
        AudioManager.PlaySound("Button");

        if(!selectedLanguage.transform.GetChild(0).gameObject.activeInHierarchy)
            selectedLanguage.transform.GetChild(0).gameObject.SetActive(true);
        else
        {
            isHidden = false;
            InteractableControl(true);
            selectedLanguage.transform.GetChild(0).gameObject.SetActive(false);
        }

        FadeOtherSettings();
    }

    private void FadeOtherSettings()
    {
        float alphaValue = isHidden == true ? 0.1f : 1f;
        settingsGroup.DOFade(alphaValue, 0.15f);
    }

    public void Bahasa()
    {
        AudioManager.PlaySound("Button");
        selectedLanguageText.text = "Indonesia";
        settingsGroup.DOFade(1f, 0.15f);
        selectedLanguage.transform.GetChild(0).gameObject.SetActive(false);
        InteractableControl(true);

        //Semua bahasa berubah Indonesia
    }

    public void English()
    {
        AudioManager.PlaySound("Button");
        selectedLanguageText.text = "English";
        settingsGroup.DOFade(1f, 0.15f);
        selectedLanguage.transform.GetChild(0).gameObject.SetActive(false);
        InteractableControl(true);

        //Semua bahasa berubah Inggris
    }


    //Music Etc.

        //Untuk atur image audio buttons setelah di set, ke game, balik lagi
    private void AudioButtonImageMenu()
    {
        if(PlayerPrefs.GetInt("Music", 1) == 0) buttonImages[0].enabled = false;
        else buttonImages[0].enabled = true;
        if(PlayerPrefs.GetInt("Sound", 1) == 0) buttonImages[1].enabled = false;
        else buttonImages[1].enabled = true;
        if(PlayerPrefs.GetInt("Vibrate", 1) == 0) buttonImages[2].enabled = false;
        else buttonImages[2].enabled = true;
    }
    public void MusicButton()
    {
        AudioManager.PlaySound("Button");
        
        Image onImage = msc_Button.transform.GetChild(0).GetComponent<Image>();

        if(onImage.enabled)
        {
            PlayerPrefs.SetInt("Music", 0);
            onImage.enabled = false;
        }
        else
        {
            PlayerPrefs.SetInt("Music", 1);
            onImage.enabled = true;
        }
    }
    public void SoundButton()
    {
        AudioManager.PlaySound("Button");
        
        Image onImage = sou_Button.transform.GetChild(0).GetComponent<Image>();

        if(onImage.enabled)
        {
            PlayerPrefs.SetInt("Sound", 0);
            onImage.enabled = false;
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
            onImage.enabled = true;
        }
    }
    public void VibrationButton()
    {
        AudioManager.PlaySound("Button");
        
        Image onImage = vib_Button.transform.GetChild(0).GetComponent<Image>();

        if(onImage.enabled)
        {
            PlayerPrefs.SetInt("Vibrate", 0);
            onImage.enabled = false;
        }
        else
        {
            PlayerPrefs.SetInt("Vibrate", 1);
            onImage.enabled = true;
        }
    }

    private void UnlockedWorldsChecker()
    {
        for(int i = 0 ; i < SaveManager.Instance.WorldsUnlocked.Length ; i++)
        {
            if(SaveManager.Instance.WorldsUnlocked[i] == true)
            {
                ShopSystem.Instance.worldPlayButtons[i].interactable = true;
                
                if(ShopSystem.Instance.padlocks[i] != null)
                ShopSystem.Instance.padlocks[i].SetActive(false);
            }
            else
            {
                ShopSystem.Instance.worldPlayButtons[i].interactable = false;

                if(ShopSystem.Instance.padlocks[i] != null)
                ShopSystem.Instance.padlocks[i].SetActive(true);
            }
        }
    }
    private IEnumerator Pulse()
    {   
        AudioManager.PlaySound("UpdateGold");

        for(float i = 1f; i <= 1.25f ; i += 0.025f)
        {
            walletText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        walletText.rectTransform.localScale = new Vector3(1f, 1f, 1f);

        for(float i = 1.25f; i >= 1f ; i -= 0.025f)
        {
            walletText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        walletText.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }
}

