using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSystem : MonoBehaviour
{
    public static ShopSystem Instance {get; private set;}
    public int[] worldsPrice = new int[5];
    public GameObject play; //TapOnPlay GameObject
    public Button buy; //PriceBox
    public TextMeshProUGUI price; // Price Text

    [Header("Worlds Play Button")]
    public Button[] worldPlayButtons = new Button[5];
    [Header("Padlocks")]
    public GameObject[] padlocks = new GameObject[3];

    private void Awake() {
        Instance = this;
        
        foreach(Button b in worldPlayButtons)
        {
            b.onClick.AddListener(TaskOnClick);
        }
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
}
