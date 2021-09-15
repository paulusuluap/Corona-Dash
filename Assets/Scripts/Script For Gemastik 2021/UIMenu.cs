using UnityEngine;
using TMPro;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    [Header("Texts")]
    public TextMeshProUGUI walletText;
    public TextMeshProUGUI walletOnUpgradesText;
    public TextMeshProUGUI selectedLanguageText;
    public TextMeshProUGUI musicText;
    public TextMeshProUGUI soundText;
    public TextMeshProUGUI vibrationText;
    public TextMeshProUGUI bahasaText;
    public TextMeshProUGUI tapText;
    public TextMeshProUGUI[] highScoreTexts = new TextMeshProUGUI[5]; //Nanti dicari metode lain    

    [Header("Rect Transforms")]
    public RectTransform mainMenu;
    public RectTransform settingsMenu;
    public RectTransform powerUpsMenu;
    public RectTransform infoMenu;
    public RectTransform fade;

    [Header("Languages Button")]
    public Button selectedLanguage;
    public Button bahasa;
    public Button english;

    [Header("Music Buttons & Others")]
    public CanvasGroup settingsGroup;
    public Button exitSetting_Button;
    public Button msc_Button;
    public Button sou_Button;
    public Button vib_Button;
    public Image[] buttonImages = new Image[3];
    
    [Header("Upgrades")]
    [SerializeField] private Button m_UpgradeButton; //Petir di Menu
    [SerializeField] private Button m_PanelUpgradeButton; //if upgrade panel is click return
    [SerializeField] private Button m_ExitUpgradeButton; // fungsi sama ky panelUpgrade

    [Header("Character Info")]
    [SerializeField] private TextMeshProUGUI m_CharacterName;
    [SerializeField] private TextMeshProUGUI m_CharacterDesc;    
    [SerializeField] private TextMeshProUGUI m_CloseCharacter;    
    [SerializeField] private Button m_OpenInfoButton_1; 
    [SerializeField] private Button m_OpenInfoButton_2; 
    [SerializeField] private Button m_OpenInfoButton_3; 
    [SerializeField] private Button m_OpenInfoButton_4; 
    [SerializeField] private Button m_ExitInfoButton; 
    [SerializeField] private Button m_ExitInfoButtonPanel; 
    [SerializeField] private Image m_iconSprite; 
    [SerializeField] private Sprite[] m_Sprites = new Sprite[4]; 


    private float myMoney, updatedMoney, addedMoney; //initMoney tempat store myMoney awal awake
    private float textUpdateTime = 1.5f;
    private bool isMoneyUpdated = false;
    private bool isHidden = false; // for settings UI
    
    
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

        SaveManager.Instance.playerMoney = Mathf.RoundToInt(myMoney + addedMoney);
        SaveManager.Instance.gainedMoney = 0;
        SaveManager.Instance.Save();

        updatedMoney = SaveManager.Instance.playerMoney;
        
        SetLanguage(SaveManager.Instance.language);
        
        if(addedMoney > 0) StartCoroutine(Pulse());
        else 
        {
            walletText.text = $"$ {(myMoney).ToString("0")}";
            walletOnUpgradesText.text = walletText.text;
        }

        //Tap To Play Yoyo Effect (bottomTranform are TapOnPlay and Buy Button)
        YoyoScalingLoop();
        
        //Audio Button Images On Start Menu
        AudioButtonImageMenu();

        //Add functions to upgrade buttons
        ButtonsTasksOnClick();
    }

    private void Update() {
        
        if(!isMoneyUpdated)
        UpdatingWalletAfterGame();
        UpdatingWalletAfterBuy();
        UnlockedWorldsChecker();
    }

    //Updating Wallet Smooth with Lerp
    private void UpdatingWalletAfterGame()
    {
        if(addedMoney == 0) return;        
        else
        {   
            myMoney = Mathf.Lerp(myMoney, updatedMoney, textUpdateTime * Time.deltaTime);            
            walletText.text = $"$ {Mathf.RoundToInt(myMoney).ToString("0")}";
            walletOnUpgradesText.text = walletText.text;

            if(Mathf.RoundToInt(myMoney) == Mathf.RoundToInt(updatedMoney))
            {
                isMoneyUpdated = true;
                return;   
            }
        }
    }

    private void UpdatingWalletAfterBuy()
    {
        if(SaveManager.Instance.playerMoney == Mathf.RoundToInt(updatedMoney)) 
        return;
        
        if(SaveManager.Instance.playerMoney < Mathf.RoundToInt(updatedMoney))
        {
            updatedMoney = Mathf.Lerp(updatedMoney, SaveManager.Instance.playerMoney, textUpdateTime * Time.deltaTime);
            walletText.text = $"$ {Mathf.Round(updatedMoney).ToString("0")}";
            walletOnUpgradesText.text = walletText.text;
        }
    }

    //Yoyo effect for play & buy image
    private void YoyoScalingLoop()
    {
        //Play
        ShopSystem.Instance.play.transform.DOScale(new Vector3(18.5f, 18.5f, 1f), 1.5f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        //Buy
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
        AudioManager.PlaySound("OpenUI");
        mainMenu.DOAnchorPos(new Vector2(0f, 2300f), 0.25f).SetEase(Ease.InExpo);
        settingsMenu.DOAnchorPos(new Vector2(0f, 0f), 0.25f).SetDelay(0.25f).SetEase(Ease.OutExpo);
    }
    public void ExitSettings()
    {
        AudioManager.PlaySound("CloseUI");
        settingsMenu.DOAnchorPos(new Vector2(-1100f, 0f), 0.25f).SetEase(Ease.InExpo);
        mainMenu.DOAnchorPos(new Vector2(0f, 0f), 0.25f).SetDelay(0.25f).SetEase(Ease.OutExpo);
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
        settingsGroup.DOFade(1f, 0.15f);
        selectedLanguage.transform.GetChild(0).gameObject.SetActive(false);
        InteractableControl(true);

        //Semua bahasa berubah Indonesia
        SaveManager.Instance.language = "Indonesia";
        SaveManager.Instance.Save();

        SetLanguage(SaveManager.Instance.language);
    }
    public void English()
    {
        AudioManager.PlaySound("Button");
        settingsGroup.DOFade(1f, 0.15f);
        selectedLanguage.transform.GetChild(0).gameObject.SetActive(false);
        InteractableControl(true);

        //Semua bahasa berubah Inggris
        SaveManager.Instance.language = "English";
        SaveManager.Instance.Save();

        SetLanguage(SaveManager.Instance.language);
    }

    private void SetLanguage(string bahasa)
    {
        if(bahasa == "English")
        {
            selectedLanguageText.text = "English";
            bahasaText.text = "Language";
            musicText.text = "Music";
            soundText.text = "Sound";
            vibrationText.text = "Vibration";
            tapText.text = "Tap To Play";
        }
        else
        {
            selectedLanguageText.text = "Indonesia";
            bahasaText.text = "Bahasa";
            musicText.text = "Musik";
            soundText.text = "Suara";
            vibrationText.text = "Getaran";
            tapText.text = "Ketuk Untuk Bermain";
        }

        ShopSystem.Instance.UpgradesLanguage(bahasa);
    }    

    private void ButtonsTasksOnClick()
    {
        m_UpgradeButton.onClick.AddListener(OpenUpgradePowerUpsPanel); 
        m_PanelUpgradeButton.onClick.AddListener(CloseUpgradePowerUpsPanel);
        m_ExitUpgradeButton.onClick.AddListener(CloseUpgradePowerUpsPanel);        

        //Info Button
        m_OpenInfoButton_1.onClick.AddListener(delegate{OpenCharacterInfoPanel(1);});
        m_OpenInfoButton_2.onClick.AddListener(delegate{OpenCharacterInfoPanel(2);});
        m_OpenInfoButton_3.onClick.AddListener(delegate{OpenCharacterInfoPanel(3);});
        m_OpenInfoButton_4.onClick.AddListener(delegate{OpenCharacterInfoPanel(4);});
        m_ExitInfoButton.onClick.AddListener(CloseCharacterInfoPanel);
        m_ExitInfoButtonPanel.onClick.AddListener(CloseCharacterInfoPanel);
    }

    // Character Info Panel
    protected void OpenCharacterInfoPanel(int characterIndex)
    {        
        AudioManager.PlaySound("OpenUI");        
        m_CloseCharacter.text = SaveManager.Instance.language == "Indonesia" 
            ? "Tutup" : "Close";

        //Text & Name
        switch(characterIndex)
        {
            case 1:
            m_CharacterName.text = "Andre";
            m_iconSprite.sprite = m_Sprites[0];
            m_CharacterDesc.text = SaveManager.Instance.language == "Indonesia" 
                ? "Pengusaha yang mempertahankan usahanya demi hidup karyawan dan keluarganya ditengah pandemi Covid-19." 
                : "An entrepreneur who maintains his business for the life of his employees and his family in the midst of the Covid-19 pandemic.";
            break;
            case 2:
            m_CharacterName.text = "Joko";
            m_iconSprite.sprite = m_Sprites[1];
            m_CharacterDesc.text = SaveManager.Instance.language == "Indonesia" 
                ? "Pekerja bangunan yang memutuskan untuk tidak menyerah ditengah pandemi dan tetap memilih bekerja untuk keluarganya."
                : "Construction worker who decided not to give up in the middle of the pandemic and still choose to work for his family.";
            break;
            case 3:
            m_CharacterName.text = "Dr. Winda";
            m_iconSprite.sprite = m_Sprites[2];
            m_CharacterDesc.text = SaveManager.Instance.language == "Indonesia" 
                ? "Dokter yang mendedikasikan hidupnya dan mempertaruhkan nyawanya untuk kesehatan masyarakat dan memutuskan tetap bekerja di tengah pandemi Covid-19." 
                : "A doctor who dedicated her life and risked her life for public health and decided to keep working in the midst of the Covid-19 pandemic.";
            break;
            case 4:
            m_CharacterName.text = "Pak Bejo";
            m_iconSprite.sprite = m_Sprites[3];
            m_CharacterDesc.text = SaveManager.Instance.language == "Indonesia" 
                ? "Petani yang tidak menyerah menghasilkan hasil pertanian terbaik dimasa pandemi." 
                : "Farmer who does not give up producing the best agricultural products during the pandemic.";
            break;
        }

        infoMenu.DOAnchorPos(new Vector2(0f, 0f), 0.25f)
            .SetEase(Ease.OutExpo)
            .SetDelay(0.25f);        
    }

    protected void CloseCharacterInfoPanel()
    {
        AudioManager.PlaySound("CloseUI");

        infoMenu.DOAnchorPos(new Vector2(0f, -2250f), 0.25f).SetEase(Ease.InExpo);
    }    

    //Power Ups Upgrade UI
    protected void OpenUpgradePowerUpsPanel()
    {
        float panelAlphaValue = 1f;
        AudioManager.PlaySound("OpenUI");

        // mainMenu.DOMove(new Vector2(0f, 2300f), 0.25f); //acuanBuatFungsiDotween
        mainMenu.DOAnchorPos(new Vector2(0f, 2300f), 0.25f).SetEase(Ease.InExpo);

        m_PanelUpgradeButton.gameObject.SetActive(true);
        m_PanelUpgradeButton.GetComponent<CanvasGroup>().alpha = 0;
        m_PanelUpgradeButton.GetComponent<CanvasGroup>().DOFade(panelAlphaValue, 0.25f);

        powerUpsMenu.DOAnchorPos(new Vector2(0f, 0f), 0.25f)
            .SetEase(Ease.OutExpo)
            .SetDelay(0.25f);        
    }
    
    protected void CloseUpgradePowerUpsPanel()
    {
        AudioManager.PlaySound("CloseUI");

        powerUpsMenu.DOAnchorPos(new Vector2(0f, -2250f), 0.25f).SetEase(Ease.InExpo);
        m_PanelUpgradeButton.GetComponent<CanvasGroup>()
            .DOFade(0f, 0.25f)
            .OnComplete(()=> m_PanelUpgradeButton.gameObject.SetActive(false));

        mainMenu.DOAnchorPos(new Vector2(0f, 0f), 0.25f)
            .SetDelay(0.25f)
            .SetEase(Ease.OutExpo);
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
            ShopSystem.Instance.worldPlayButtons[i].interactable = true;
            
            else
            ShopSystem.Instance.worldPlayButtons[i].interactable = false;                            
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

