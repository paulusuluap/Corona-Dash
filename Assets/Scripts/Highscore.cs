using UnityEngine;
using TMPro;

public class Highscore : MonoBehaviour
{  
    [SerializeField] private TextMeshProUGUI _newHighScore;
    float _highScore;
    void Start()
    {
        if(PlayerPrefs.HasKey("HighScore"))
            _highScore = PlayerPrefs.GetFloat("HighScore", 60.1f);

        _newHighScore.text = (Mathf.Round(_highScore * 10f)/10f).ToString() + " s";
    }   

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        _newHighScore.text = "0 s";
    }
}