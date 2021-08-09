using UnityEngine;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

namespace MainMenu {
    public class UIMenu : MonoBehaviour
    {
        private float myMoney, initMoney, addedMoney; //initMoney tempat store myMoney awal awake
        private float moneyUpdateTime = 1.5f;
        private bool isMoneyUpdated = false;
        private bool isHidden = false; // for settings UI
        public Transform tapOnPlayTransform, centre; //character hanya percobaan satu stage
        public CanvasGroup settingsGroup;
        [Header("Texts")]
        public TextMeshProUGUI walletText;
        public TextMeshProUGUI selectedLanguageText = default;
        public TextMeshProUGUI[] highScoreTexts = new TextMeshProUGUI[5]; //Nanti dicari metode lain

        [Header("Rect Transforms")]
        public RectTransform mainMenu;
        public RectTransform settingsMenu;
        public RectTransform powerUpsMenu;
        public RectTransform fade;

        [Header("Languages")]
        public Button selectedLanguage;
        public Button bahasa;
        public Button english;
        [Header("Buttons")]
        public Button msc_Button;
        public Button sou_Button;
        public Button vib_Button;
        public Image[] buttonImages = new Image[3];
        

        private void Awake() {
            DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(2000, 100);

            //Initial Money
            myMoney = PlayerPrefs.HasKey("MyWallet") ? PlayerPrefs.GetInt("MyWallet") : 0;
            initMoney = myMoney;

            //Added Money
            addedMoney = PlayerPrefs.HasKey("MoneyCollected") ? PlayerPrefs.GetInt("MoneyCollected", 0) : 0;

            //Load HighScore
            for(int i = 0 ; i < SceneManagerScript.instance.numberOfWorlds ; i++)
            highScoreTexts[i].text = PlayerPrefs.GetInt("Highscore"+(i+1).ToString()).ToString();
        }

        private void Start() {

            //Tap To Play Yoyo Effect
            tapOnPlayTransform.DOScale(new Vector3(18.5f, 18.5f, 1f), 1.5f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);

            //Audio Button Images On Start Menu
            AudioButtonImageMenu();
        }

        private void Update() {
            if(!isMoneyUpdated) UpdatingWallet();
        }

        //Updating Wallet Smooth with Lerp
        private void UpdatingWallet()
        {
            myMoney = Mathf.Lerp(myMoney, (initMoney + addedMoney), moneyUpdateTime * Time.deltaTime);
            walletText.text = (myMoney).ToString("0");

            if(myMoney >= initMoney + addedMoney)
            {
                PlayerPrefs.SetInt("MyWallet", (int) myMoney);
                PlayerPrefs.DeleteKey("MoneyCollected");
                isMoneyUpdated = true;
                return;
            }
        }

        //Stage Selection
        public void FadeSceneTransition (int index)
        {
            AudioManager.PlaySound("Button");
            PlayerPrefs.SetInt("FromMenu", 1);
            
            //Change DOScale below with black transition
            fade.DOAnchorPos(new Vector2(0f, 0f), 1.5f).SetEase(Ease.InSine)
                .OnComplete(()=> SceneManagerScript.instance.LoadWorld(index));
        }

        // Settings

        public void SettingsMenu()
        {
            AudioManager.PlaySound("Button");
            mainMenu.DOAnchorPos(new Vector2(0f, 2000f), 0.25f);
            settingsMenu.DOAnchorPos(new Vector2(0f, 0f), 0.25f).SetDelay(0.25f);
        }
        public void BackFromSettingsMenu()
        {
            AudioManager.PlaySound("Button");
            settingsMenu.DOAnchorPos(new Vector2(-1100f, 0f), 0.25f);
            mainMenu.DOAnchorPos(new Vector2(0f, 0f), 0.25f).SetDelay(0.25f);
        }

        public void SelectLanguage()
        {
            isHidden = true;
            AudioManager.PlaySound("Button");

            if(!selectedLanguage.transform.GetChild(0).gameObject.activeInHierarchy)
                selectedLanguage.transform.GetChild(0).gameObject.SetActive(true);
            else
            {
                isHidden = false;
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

            //Semua bahasa berubah Indonesia
        }

        public void English()
        {
            AudioManager.PlaySound("Button");
            selectedLanguageText.text = "English";
            settingsGroup.DOFade(1f, 0.15f);
            selectedLanguage.transform.GetChild(0).gameObject.SetActive(false);

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
    }
}
