using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ShopSystem : MonoBehaviour
{
    public static ShopSystem Instance {get; private set;}
    public int[] worldsPrice = new int[5];
    private int[] upgradePrice = new int[5] {50, 100, 200, 350, 500};
    public int permittedWorlds = 4;
    private int maxUpgradeLevel = 5;
    private string secondText;
    public GameObject play; //TapOnPlay GameObject
    public Button buy; //PriceBox
    public TextMeshProUGUI price; // Price Text

    [Header("Worlds Play Button")]
    public Button[] worldPlayButtons = new Button[5];
    
    [Header("Upgrades Price Buttons")]
    [SerializeField] private Button m_MagnetUpgradeButton; 
    [SerializeField] private Button m_MaskUpgradeButton; 
    [SerializeField] private Button m_InvincibleUpgradeButton; 
    [SerializeField] private Button m_MultiplierUpgradeButton; 

    [Header("Upgrades Price UI")]
    [SerializeField] private TextMeshProUGUI magnetPrice;
    [SerializeField] private TextMeshProUGUI maskPrice;
    [SerializeField] private TextMeshProUGUI invinciblePrice;
    [SerializeField] private TextMeshProUGUI multiplierPrice;

    [Header("Upgrades Duration UI")]
    [SerializeField] private TextMeshProUGUI magnetDuration;
    [SerializeField] private TextMeshProUGUI maskDuration;
    [SerializeField] private TextMeshProUGUI invincibleDuration;
    [SerializeField] private TextMeshProUGUI multiplierDuration;

    [Header("Power Ups Descriptions")]
    [SerializeField] private TextMeshProUGUI magnetDesc;
    [SerializeField] private TextMeshProUGUI maskDesc;
    [SerializeField] private TextMeshProUGUI invincibleDesc;
    [SerializeField] private TextMeshProUGUI multiplierDesc;

    [Header("Upgrades Progression UI")]
    [SerializeField] private Transform magnetProgress;
    [SerializeField] private Transform maskProgress;
    [SerializeField] private Transform invincibleProgress;
    [SerializeField] private Transform multiplierProgress;
    private List<GameObject> magnetProgressionImages = new List<GameObject>();
    private List<GameObject> maskProgressionImages = new List<GameObject>();
    private List<GameObject> invincibleProgressionImages = new List<GameObject>();
    private List<GameObject> multiplierProgressionImages = new List<GameObject>();



    private void Awake() {
        Instance = this;
        
        foreach(Button b in worldPlayButtons)
        {
            b.onClick.AddListener(TaskOnClick);
        }

        UpgradesButtonsCallback();
        UpgradesTextsOnStart(); 
        UpgradeProgressions(); 
    }

    private void TaskOnClick()
    {
        foreach(Button b in worldPlayButtons)
        b.enabled = false;
    }

    public void BuyWorld ()
    {
        if(SaveManager.Instance.playerMoney >= worldsPrice[swipe.instance.currentIndex])
        {
            AudioManager.PlaySound("Buy");
            SaveManager.Instance.playerMoney -= worldsPrice[swipe.instance.currentIndex];
            SaveManager.Instance.WorldsUnlocked[swipe.instance.currentIndex] = true;
            SaveManager.Instance.Save();
        } 
        else AudioManager.PlaySound("Button");
    }

    private void UpgradesButtonsCallback()
    {
        m_MagnetUpgradeButton.onClick.AddListener(UpgradeMagnet); 
        m_MaskUpgradeButton.onClick.AddListener(UpgradeMask); 
        m_InvincibleUpgradeButton.onClick.AddListener(UpgradeInvincible); 
        m_MultiplierUpgradeButton.onClick.AddListener(UpgradeMultiplier);
    }

    private void UpgradesTextsOnStart()
    {
        m_MagnetUpgradeButton.interactable = SaveManager.Instance.MagnetLevel < maxUpgradeLevel ? true : false;
        m_MaskUpgradeButton.interactable = SaveManager.Instance.MaskLevel < maxUpgradeLevel ? true : false;
        m_InvincibleUpgradeButton.interactable = SaveManager.Instance.InvincibleLevel < maxUpgradeLevel ? true : false;
        m_MultiplierUpgradeButton.interactable = SaveManager.Instance.MultiplierLevel < maxUpgradeLevel ? true : false;

        magnetPrice.text = SaveManager.Instance.MagnetLevel < maxUpgradeLevel ? $"$ {upgradePrice[SaveManager.Instance.MagnetLevel].ToString()}" : magnetPrice.text = "Max";
        maskPrice.text = SaveManager.Instance.MaskLevel < maxUpgradeLevel ? $"$ {upgradePrice[SaveManager.Instance.MaskLevel].ToString()}" : maskPrice.text = "Max";
        invinciblePrice.text = SaveManager.Instance.InvincibleLevel < maxUpgradeLevel ? $"$ {upgradePrice[SaveManager.Instance.InvincibleLevel].ToString()}" : invinciblePrice.text = "Max";
        multiplierPrice.text = SaveManager.Instance.MultiplierLevel < maxUpgradeLevel ? $"$ {upgradePrice[SaveManager.Instance.MultiplierLevel].ToString()}" : multiplierPrice.text = "Max";

        secondText = SaveManager.Instance.language == "English" ? "sec" : "detik";

        magnetDuration.text = $"{SaveManager.Instance.MagnetDuration} {secondText}";
        maskDuration.text = $"{SaveManager.Instance.MaskDuration} {secondText}";
        invincibleDuration.text = $"{SaveManager.Instance.InvincibleDuration} {secondText}";
        multiplierDuration.text = $"{SaveManager.Instance.MultiplierDuration} {secondText}";
    }

    private void UpgradeProgressions()
    {
        //Adding Progression Parent's children to Lists
        for(int i = 0 ; i < maxUpgradeLevel ; i++)
        {
            magnetProgressionImages.Add(magnetProgress.transform.GetChild(i).gameObject);
            maskProgressionImages.Add(maskProgress.transform.GetChild(i).gameObject);
            invincibleProgressionImages.Add(invincibleProgress.transform.GetChild(i).gameObject);
            multiplierProgressionImages.Add(multiplierProgress.transform.GetChild(i).gameObject);                 
        }

        UpgradeBarProgressActivation();
    }

    private void UpgradeBarProgressActivation()
    {        
        for(int i = 0; i < maxUpgradeLevel; i++)        
        {                                 
            if(i >= SaveManager.Instance.MagnetLevel)
            magnetProgressionImages[i].SetActive(false);
            else 
            magnetProgressionImages[i].SetActive(true);

            if(i >= SaveManager.Instance.MaskLevel)
            maskProgressionImages[i].SetActive(false);
            else 
            maskProgressionImages[i].SetActive(true);

            if(i >= SaveManager.Instance.InvincibleLevel)
            invincibleProgressionImages[i].SetActive(false);
            else 
            invincibleProgressionImages[i].SetActive(true);

            if(i >= SaveManager.Instance.MultiplierLevel)
            multiplierProgressionImages[i].SetActive(false);
            else 
            multiplierProgressionImages[i].SetActive(true);
        }
        
    }

    //UPGRADE BUTTONS

    //Button to Upgrade Magnet Power Up
    private void UpgradeMagnet()
    {        
        AudioManager.PlaySound("UpgradePowerUp");
        
        if(SaveManager.Instance.playerMoney >= upgradePrice[SaveManager.Instance.MagnetLevel]
            && SaveManager.Instance.MagnetLevel < maxUpgradeLevel)
        {
            SaveManager.Instance.playerMoney -= upgradePrice[SaveManager.Instance.MagnetLevel];
                                
            SaveManager.Instance.MagnetLevel++;
            SaveManager.Instance.MagnetDuration += 2f;                        

            SaveManager.Instance.Save();

            if(SaveManager.Instance.MagnetLevel == maxUpgradeLevel)
            {
                magnetPrice.text = "Max";
                magnetDuration.text = $"{SaveManager.Instance.MagnetDuration} {secondText}";
                m_MagnetUpgradeButton.interactable = false;                
            }
            else
            {
                magnetPrice.text = $"$ {upgradePrice[SaveManager.Instance.MagnetLevel].ToString()}";
                magnetDuration.text = $"{SaveManager.Instance.MagnetDuration} {secondText}"; //gmana ke bahasa indonesia            
            }

            UpgradeBarProgressActivation();
        }        
    }
    //Button to Upgrade Super Mask Power Up
    private void UpgradeMask()
    {
        AudioManager.PlaySound("UpgradePowerUp");

        if(SaveManager.Instance.playerMoney >= upgradePrice[SaveManager.Instance.MaskLevel]
            && SaveManager.Instance.MaskLevel < maxUpgradeLevel)
        {
            SaveManager.Instance.playerMoney -= upgradePrice[SaveManager.Instance.MaskLevel];
                                
            SaveManager.Instance.MaskLevel++;
            SaveManager.Instance.MaskDuration += 2f;                        

            SaveManager.Instance.Save();

            if(SaveManager.Instance.MaskLevel == maxUpgradeLevel)
            {
                maskPrice.text = "Max";
                maskDuration.text = $"{SaveManager.Instance.MaskDuration} {secondText}";
                m_MaskUpgradeButton.interactable = false;                
            }
            else
            {
                maskPrice.text = $"$ {upgradePrice[SaveManager.Instance.MaskLevel].ToString()}";
                maskDuration.text = $"{SaveManager.Instance.MaskDuration} {secondText}"; //gmana ke bahasa indonesia            
            }

            UpgradeBarProgressActivation();
        }
    }
    //Button to Upgrade Invincible Power Up
    private void UpgradeInvincible()
    {
        AudioManager.PlaySound("UpgradePowerUp");
        
        if(SaveManager.Instance.playerMoney >= upgradePrice[SaveManager.Instance.InvincibleLevel]
            && SaveManager.Instance.InvincibleLevel < maxUpgradeLevel)
        {
            SaveManager.Instance.playerMoney -= upgradePrice[SaveManager.Instance.InvincibleLevel];
                                
            SaveManager.Instance.InvincibleLevel++;
            SaveManager.Instance.InvincibleDuration += 2f;                        

            SaveManager.Instance.Save();

            if(SaveManager.Instance.InvincibleLevel == maxUpgradeLevel)
            {
                invinciblePrice.text = "Max";
                invincibleDuration.text = $"{SaveManager.Instance.InvincibleDuration} {secondText}";
                m_InvincibleUpgradeButton.interactable = false;                
            }
            else
            {
                invinciblePrice.text = $"$ {upgradePrice[SaveManager.Instance.InvincibleLevel].ToString()}";
                invincibleDuration.text = $"{SaveManager.Instance.InvincibleDuration} {secondText}"; //gmana ke bahasa indonesia            
            }

            UpgradeBarProgressActivation();
        }
    }
    //Button to Upgrade Multiplier Power Up
    private void UpgradeMultiplier()
    {
        AudioManager.PlaySound("UpgradePowerUp");

        if(SaveManager.Instance.playerMoney >= upgradePrice[SaveManager.Instance.MultiplierLevel]
            && SaveManager.Instance.MultiplierLevel < maxUpgradeLevel)
        {
            SaveManager.Instance.playerMoney -= upgradePrice[SaveManager.Instance.MultiplierLevel];
                                
            SaveManager.Instance.MultiplierLevel++;
            SaveManager.Instance.MultiplierDuration += 2f;                        

            SaveManager.Instance.Save();

            if(SaveManager.Instance.MultiplierLevel == maxUpgradeLevel)
            {
                multiplierPrice.text = "Max";
                multiplierDuration.text = $"{SaveManager.Instance.MultiplierDuration} {secondText}";
                m_MultiplierUpgradeButton.interactable = false;                
            }
            else
            {
                multiplierPrice.text = $"$ {upgradePrice[SaveManager.Instance.MultiplierLevel].ToString()}";
                multiplierDuration.text = $"{SaveManager.Instance.MultiplierDuration} {secondText}"; //gmana ke bahasa indonesia            
            }

            UpgradeBarProgressActivation();
        }
    }

    public void UpgradesLanguage(string language)
    {
        if(language == "English")
        {
            magnetDesc.text = "Make coins around you fly towards you.";
            maskDesc.text = "Protect you from Covid-19 viruses.";
            invincibleDesc.text = "You can go through buildings and viruses with this.";
            multiplierDesc.text = "Each time you get the coin, it will multiply by two.";

            secondText = "sec";
            magnetDuration.text = $"{SaveManager.Instance.MagnetDuration} {secondText}";
            maskDuration.text = $"{SaveManager.Instance.MaskDuration} {secondText}";
            invincibleDuration.text = $"{SaveManager.Instance.InvincibleDuration} {secondText}";
            multiplierDuration.text = $"{SaveManager.Instance.MultiplierDuration} {secondText}";
        }
        else
        {
            magnetDesc.text = "Membuat koin disekitar mu terbang ke arah mu.";
            maskDesc.text = "Melindungi mu dari virus Covid-19.";
            invincibleDesc.text = "Kamu dapat menembus bangunan dan virus dengan ini.";
            multiplierDesc.text = "Setiap kali kamu dapat koin, koin akan dikalikan dua.";

            secondText = "detik";
            magnetDuration.text = $"{SaveManager.Instance.MagnetDuration} {secondText}";
            maskDuration.text = $"{SaveManager.Instance.MaskDuration} {secondText}";
            invincibleDuration.text = $"{SaveManager.Instance.InvincibleDuration} {secondText}";
            multiplierDuration.text = $"{SaveManager.Instance.MultiplierDuration} {secondText}";
        }
    }
}
