using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager current;

    [Header("Texts")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI newHighScoreText; //
    public TextMeshProUGUI deathScoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI prizeText;
    public TextMeshProUGUI nextLevelNotifText; //
    public TextMeshProUGUI restartText;

    [Header("Scores")]
    private int score;
    public int Score { get{return score; } set {value = score;}}
    private int lastHighScore;
    private int highscoreIndex;

    [Header("Others")]
    public GameObject powerUpsIcon;
    public GameObject deathPopUp;
    public GameObject gameplayUI;
    public List<GameObject> powIconStorage;
    public Slider powSlider;
    public RectTransform fader;
    private float resetSlider = 10f;
    private bool isNewScore = false;
    private string sceneName;
    public string SceneName{get { return sceneName;}}
    public ParticleSystem[] fireworks = new ParticleSystem[3]; // Fireworks Trigger

    private void Awake() {
        current = this;
        sceneName = SceneManager.GetActiveScene().name;
        
        Collectibles.CoinPickedUp += Pulsing;
        score = 0;

        FadeSetter();

        //Get the last highscore for each scene
        for(int i = 1 ; i < 6 ; i++) 
        {
            if(sceneName == "World_"+i.ToString())
            {
                lastHighScore =  PlayerPrefs.HasKey("Highscore_" + i.ToString()) 
                    ? PlayerPrefs.GetInt("Highscore_" + i.ToString(), 0) : 0;

                highscoreIndex = i;
            }
        }
        
        newHighScoreText.gameObject.SetActive(false);
        prizeText.gameObject.SetActive(false);
        nextLevelNotifText.gameObject.SetActive(false);

        //PowerUps inside frame icons
        foreach(Transform p in powerUpsIcon.transform)
        {
            p.gameObject.SetActive(false);
            powIconStorage.Add(p.gameObject);
        }

        //Button Interactable control        
        foreach(Button b in Resources.FindObjectsOfTypeAll(typeof(Button)) as Button[])
        {
            b.enabled = true;
            b.onClick.AddListener(TaskOnClick);
        }   
    }

    private void Start() {
        SetLanguageOnStart(SaveManager.Instance.language);
    }

    private void TaskOnClick()
    {
        foreach(Button b in Resources.FindObjectsOfTypeAll(typeof(Button)) as Button[])
        b.enabled = false;
    }

    private void SetLanguageOnStart(string bahasa)
    {
        if(bahasa == "English")
        {
            newHighScoreText.text = "New Highscore";
            restartText.text = "Tap To Replay";
        }
        else
        {
            newHighScoreText.text = "Highscore Baru";
            restartText.text = "Ketuk Untuk Mengulang";
        }
    }

    private void Update() {
        scoreText.text = score.ToString("000");

        BeatTheHighScore();
        NextLevelNotification();
    }

    #region FadeTransition
    private void FadeSetter()
    {
        if(PlayerPrefs.GetInt("FromMenu") == 1) 
        {
            fader.gameObject.SetActive(true);
            Invoke("Fade", 0.5f);
        } else return;
    }

    private void Fade()
    {   
        PlayerPrefs.SetInt("FromMenu", 0);

        fader.DOAnchorPos(new Vector2(2500f, 0f), 1.5f).SetEase(Ease.OutSine)
            .OnComplete(()=> fader.gameObject.SetActive(false));
    }
    #endregion

    //Coin Counter Pulse Effect
    private IEnumerator CoinPulse()
    {   
        for(float i = 1f; i <= 1.15f ; i += 0.05f)
        {
            scoreText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        scoreText.rectTransform.localScale = new Vector3(1f, 1f, 1f);

        score++;

        for(float i = 1.15f; i >= 1f ; i -= 0.05f)
        {
            scoreText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        scoreText.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    //Pulse for other text than coin
    private IEnumerator Pulse(TextMeshProUGUI text)
    {   
        for(float i = 1f; i <= 1.5f ; i += 0.05f)
        {
            text.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        text.rectTransform.localScale = new Vector3(1f, 1f, 1f);

        for(float i = 1.5f; i >= 1f ; i -= 0.05f)
        {
            text.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        text.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void Pulsing()
    {
        StartCoroutine(CoinPulse());
    }

    private void OnDestroy() {
        Collectibles.CoinPickedUp -= Pulsing;
    }

    public void MagnetIconCalled(float magnetDur)
    {
        if(magnetDur >= 0)
        {
            powIconStorage[0].SetActive(true);
            powIconStorage[2].SetActive(true);
            powSlider.value = magnetDur;
        }
        else
        {
            powIconStorage[0].SetActive(false);
            powIconStorage[2].SetActive(false);
            powSlider.value = resetSlider;
            return;
        };
    }

    public void InvincibleIconCalled(float invisibleDur)
    {
        if(invisibleDur >= 0)
        {
            powIconStorage[1].SetActive(true);
            powIconStorage[2].SetActive(true);
            powSlider.value = invisibleDur;
        }
        else
        {
            powIconStorage[1].SetActive(false);
            powIconStorage[2].SetActive(false);
            powSlider.value = resetSlider;
            return;
        };
    }

    public void EndUI()
    {
        gameplayUI.SetActive(false);
        StartCoroutine(DeathPopUp());
    }

    //Button When Die
    public void UpdatingWalletWhenBackHome(){
        SaveManager.Instance.gainedMoney += score;
        SaveManager.Instance.Save();
        AudioManager.PlaySound("Button");
        StartCoroutine(BackHome());
    }
    public void UpdatingWalletWhenRestart(){
        SaveManager.Instance.gainedMoney += score;
        SaveManager.Instance.Save();
        AudioManager.PlaySound("Button");
        StartCoroutine(RestartScene());
    }

    //Check if Highscore is beaten
    void BeatTheHighScore()
    {
        if(score < lastHighScore) return;

        if(score > lastHighScore && !isNewScore)
        {
            StartCoroutine(NewHighScore());
            isNewScore = true;
        }
    }

    IEnumerator NewHighScore()
    {
        newHighScoreText.gameObject.SetActive(true);

        StartCoroutine(Pulse(newHighScoreText));

        newHighScoreText.GetComponent<RectTransform>().DOAnchorPosY(-100f, 7f);

        //Cinemachine shake
        CinemachineShake.Instance.ShakeCamera(3f, 0.2f);
        //Firework
        foreach(ParticleSystem f in fireworks)
        {
            f.Play();
        }

        //PlaySound
        AudioManager.PlaySound("NewHighScore");
        
        yield return new WaitForSeconds(3f);

        newHighScoreText.GetComponent<CanvasGroup>().DOFade(0, 1f);

        yield return new WaitForSeconds(1.2f);

        newHighScoreText.gameObject.SetActive(false);
    }

    IEnumerator GetPrizeBox(int prize)
    {
        prizeText.gameObject.SetActive(true);
        prizeText.text = prize.ToString() + " $";

        StartCoroutine(Pulse(prizeText));
        CinemachineShake.Instance.ShakeCamera(3f, 0.1f);

        AudioManager.PlaySound("TakePrize");
        ParticleManager.instance.IdleParticles("PrizeTaken");

        yield return new WaitForSeconds(1f);

        prizeText.GetComponent<CanvasGroup>().DOFade(0, 1f);

        yield return new WaitForSeconds(1.2f);
        
        prizeText.GetComponent<CanvasGroup>().alpha = 1f;
        prizeText.gameObject.SetActive(false);
    }

    public void ShowPrize(int prize)
    {
        StartCoroutine(GetPrizeBox(prize));
    }

    IEnumerator DeathPopUp()
    {
        deathScoreText.text = score.ToString("000");

        //Set Highscore
        if(score > lastHighScore)
        {
            PlayerPrefs.SetInt("Highscore_" + highscoreIndex, score);
        }
        
        highScoreText.text = "HIGHSCORE " + PlayerPrefs.GetInt("Highscore_" + highscoreIndex).ToString("000");

        yield return new WaitForSecondsRealtime(2f);
        deathPopUp.SetActive(true);
    }
    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    IEnumerator BackHome()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MainMenu");
    }

    //Tells us when is the next level begin
    private void NextLevelNotification()
    {
        for(int i = 0 ; i < LevelManager.levelAmount ; i++)
        {
            if(score >= LevelManager.levelNotif[i] && score < LevelManager.newLevelScores[i])
            {
                nextLevelNotifText.gameObject.SetActive(true);

                if(SaveManager.Instance.language == "English")
                nextLevelNotifText.text = "next level in " + (LevelManager.newLevelScores[i] - score);
                else
                nextLevelNotifText.text = "level berikutnya dalam " + (LevelManager.newLevelScores[i] - score);
            }
            
            if(nextLevelNotifText.gameObject.activeInHierarchy && score == LevelManager.newLevelScores[i])
                nextLevelNotifText.gameObject.SetActive(false);

        }
    }
    
}
