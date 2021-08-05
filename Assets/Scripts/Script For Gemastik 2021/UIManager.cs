using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Header("Others")]
    public GameObject powerUpsIcon, deathPopUp, gameplayUI;
    public Slider powSlider;
    public List<GameObject> powIconStorage;
    private int score;
    public int Score { get{return score; } set {value = score;}}
    private int lastHighScore;
    private float resetSlider = 10f;
    private bool isNewScore = false;

    //Code sementara untuk firework, nanti dirapihin
    public ParticleSystem[] fireworks = new ParticleSystem[3];

    private void Awake() {
        current = this;
        Collectibles.CoinPickedUp += Pulsing;
        score = 0;
        lastHighScore = PlayerPrefs.GetInt("Highscore");
        newHighScoreText.gameObject.SetActive(false);

        foreach(Transform p in powerUpsIcon.transform)
        {
            p.gameObject.SetActive(false);
            powIconStorage.Add(p.gameObject);
        }
    }

    private void Update() {
        scoreText.text = score.ToString("000");

        BeatTheHighScore();
    }

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
        PlayerPrefs.SetInt("MoneyCollected", PlayerPrefs.GetInt("MoneyCollected", 0) + score);
        AudioManager.PlaySound("Button");
        StartCoroutine(BackHome());
    }
    public void UpdatingWalletWhenRestart(){
        PlayerPrefs.SetInt("MyWallet", PlayerPrefs.GetInt("MyWallet") + score);
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

        if(score > lastHighScore)
        PlayerPrefs.SetInt("Highscore", score);
        
        highScoreText.text = "HIGHSCORE " + PlayerPrefs.GetInt("Highscore").ToString("000");

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
