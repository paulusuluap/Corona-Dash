using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    public TextMeshProUGUI finalScore;
    public float timeCounter;
    public bool gameStop;
    private float _endScore;
    public GameObject slider;
    public GameObject joystick; 
    private float playHealth;  
    private float myHealth;
    private PlayerHealth p_health;
    private bool isWin = false;
    private Mesh WinMesh;
    private Color[] colors;

    // Update is called once per frame

    private void Start() 
    {
        p_health = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();  

        WinMesh = GameObject.FindGameObjectWithTag("EndHealth").
                    GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = WinMesh.vertices;
        colors = new Color[vertices.Length];
        
        for(int i = 0; i < vertices.Length; i++)
            colors[i] = Color.Lerp(Color.red, Color.green, vertices[i].y);

        timeCounter = 60f;
    }
    void Update()
    {
        playHealth = p_health.playerHealth;

        counterText.text = (Mathf.Round(timeCounter * 10f)/10f).ToString();        
        finalScore.text = (Mathf.Round(playHealth * 10f)/10f).ToString() + " / 100";

        if(!gameStop)
        {
            timeCounter -= Time.deltaTime;
            _endScore = 60.0f - timeCounter;
        }else if(gameStop)
        {   
            finalScore.gameObject.SetActive(true);            
            slider.SetActive(false);
            joystick.SetActive(false);
        }
        
        if(timeCounter <= 0f)
        {
            gameStop = true;
            counterText.text = "";
        }

        if(playHealth == 100f)
        {    
            gameStop = true;
            isWin = true;
            if(isWin)                
            {
                WinScoreSetter();  
                finalScore.color = finalScore.color;
                counterText.text = (Mathf.Round(_endScore * 10f)/10f).ToString();
            }
        }   

        // Final health color setter
        if(playHealth < 80f)
            finalScore.color = Color.red;

        if(playHealth >= 80f && playHealth < 100f)
            finalScore.color = Color.yellow;

        if(playHealth >= 100f)
            finalScore.color = Color.green;
    }       

    public void WinScoreSetter()
    {
        if(_endScore < PlayerPrefs.GetFloat("HighScore", 60.1f))
            PlayerPrefs.SetFloat("HighScore", _endScore);     

        PlayerPrefs.Save();   
    }
}