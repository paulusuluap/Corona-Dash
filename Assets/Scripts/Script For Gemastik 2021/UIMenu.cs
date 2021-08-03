using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using System.Collections;

public class UIMenu : MonoBehaviour
{
    public TextMeshProUGUI walletText, highScoreText;
    private int myMoney, addedMoney, prizeCollected, highscore;
    private float moneyUpdateTime = 5f;
    public Transform tapOnPlayTransform, centre;
    

    private void Awake() {
        DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(2000, 100);

        //Initial Money
        myMoney = PlayerPrefs.HasKey("MyWallet") ? PlayerPrefs.GetInt("MyWallet") : 0;
        walletText.text = myMoney.ToString();

        //Update Money
        addedMoney = PlayerPrefs.HasKey("MoneyCollected") ? PlayerPrefs.GetInt("MoneyCollected", 0) : 0;
        myMoney += addedMoney;
        PlayerPrefs.SetInt("MyWallet", myMoney);
        walletText.text = myMoney.ToString();

        //Update HighScore
        if(PlayerPrefs.HasKey("Highscore"))
        {
            highscore = PlayerPrefs.GetInt("Highscore", 0);
            highScoreText.text = highscore.ToString();
        }
    }

    private void Start() {
        tapOnPlayTransform.DOScale(new Vector3(17.5f, 17.5f, 1f), 1.5f)
			.SetEase(Ease.InOutSine)
			.SetLoops(-1, LoopType.Yoyo);
    }

    // private void Update() {
    //     PlayerPrefs.DeleteAll();
    //     // UpdatingWallet();
    // }

    private void UpdatingWallet()
    {
        //Belum berhasil
        walletText.text = Mathf.Lerp(0, myMoney, moneyUpdateTime * Time.deltaTime).ToString();
    }
    public void PlayWorld1()
    {
        //Play sound effect
        StartCoroutine(LoadWorld1());
    }
    public void PlayWorld2()
    {
        //Play sound effect
        SceneManager.LoadScene("WorldDesign2");
    }

    private IEnumerator LoadWorld1 ()
    {
        AudioManager.PlaySound("Button");
        centre.DOScale(new Vector3(0f, 0f, 0f), 0.5f)
			.SetEase(Ease.OutSine);
        
        yield return new WaitForSeconds(0.51f);

        SceneManager.LoadScene("WorldDesign1");
    }

    private IEnumerator LoadWorld2 ()
    {
        AudioManager.PlaySound("Button");
        centre.DOScale(new Vector3(0f, 0f, 0f), 0.5f)
			.SetEase(Ease.OutSine);
        
        yield return new WaitForSeconds(0.51f);

        SceneManager.LoadScene("WorldDesign2");
    }
}
