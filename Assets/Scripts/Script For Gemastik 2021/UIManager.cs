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

    [Header("Score")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI newHighScoreText;
    public TextMeshProUGUI deathScoreText;
    public TextMeshProUGUI highScoreText;
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
    public ParticleSystem[] fireworks = new ParticleSystem[3]; // Fireworks Trigger

    private void Awake() {
        current = this;
        Collectibles.CoinPickedUp += Pulsing;
        score = 0;

        FadeSetter();

        //Get Scene name
        sceneName = SceneManager.GetActiveScene().name;

        //Get the last highscore
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

        foreach(Transform p in powerUpsIcon.transform)
        {
            p.gameObject.SetActive(false);
            powIconStorage.Add(p.gameObject);
        }

        //Button Interactable control        

        foreach(Button b in Resources.FindObjectsOfTypeAll(typeof(Button)) as Button[])
        b.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        foreach(Button b in Resources.FindObjectsOfTypeAll(typeof(Button)) as Button[])
        b.enabled = false;
    }

    private void Update() {
        scoreText.text = score.ToString("000");

        BeatTheHighScore();
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
    private IEnumerator Pulse()
    {   
        for(float i = 1f; i <= 1.15f ; i += 0.05f)
        {
            scoreText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        scoreText.rectTransform.localScale = new Vector3(1f, 1f, 1f);

        score++;

        for(float i = 1.2f; i >= 1f ; i -= 0.05f)
        {
            scoreText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForEndOfFrame();
        }
        scoreText.rectTransform.localScale = new Vector3(1f, 1f, 1f);
    }

    public void Pulsing()
    {
        StartCoroutine(Pulse());
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
        //Cinemachine shake
        CinemachineShake.Instance.ShakeCamera(3f, 0.25f);
        //Firework
        foreach(ParticleSystem f in fireworks)
        {
            f.Play();
        }

        //PlaySound
        AudioManager.PlaySound("NewHighScore");
        
        yield return new WaitForSeconds(3.5f);

        newHighScoreText.gameObject.SetActive(false);
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
    
}
