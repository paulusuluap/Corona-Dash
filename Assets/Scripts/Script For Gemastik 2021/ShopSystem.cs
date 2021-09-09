using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSystem : MonoBehaviour
{
    public static ShopSystem Instance {get; private set;}
    public int[] worldsPrice = new int[5];
    private int[] upgradePrice = new int[5] {50, 100, 200, 350,500};
    private int maxUpgradeLevel = 5;
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

    private void Awake() {
        Instance = this;
        
        foreach(Button b in worldPlayButtons)
        {
            b.onClick.AddListener(TaskOnClick);
        }

        UpgradesButtonsCallback();
    }

    private void TaskOnClick()
    {
        foreach(Button b in worldPlayButtons)
        b.enabled = false;
    }

    public void BuyWorld ()
    {
        if(SaveManager.Instance.playerMoney >= ShopSystem.Instance.worldsPrice[swipe.instance.currentIndex])
        {
            AudioManager.PlaySound("Buy");
            SaveManager.Instance.playerMoney -= ShopSystem.Instance.worldsPrice[swipe.instance.currentIndex];
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

    public void UpgradeMagnet()
    {
        if(SaveManager.Instance.playerMoney >= upgradePrice[SaveManager.Instance.MagnetLevel]
            && SaveManager.Instance.MagnetLevel < maxUpgradeLevel)
        {
            SaveManager.Instance.MagnetLevel++;
            SaveManager.Instance.MagnetDuration += 2;
            
            SaveManager.Instance.playerMoney -= upgradePrice[SaveManager.Instance.MagnetLevel];
            
            magnetPrice.text = $"$ {upgradePrice[SaveManager.Instance.MagnetLevel].ToString()}";
            magnetDuration.text = $"{SaveManager.Instance.MagnetDuration} sec";
        }
    }
    public void UpgradeMask()
    {

    }
    public void UpgradeInvincible()
    {

    }
    public void UpgradeMultiplier()
    {

    }
}
