using UnityEngine;
using TMPro;

public class TEXTMANAGER : MonoBehaviour
{
    public TextMeshProUGUI winText;
    public TextMeshProUGUI failText;
    public TextMeshProUGUI almostWinText;
    public TextMeshProUGUI restart;
    public TextMeshProUGUI mainMenu;
    private PlayerHealth p_health;
    private UI_Manager t_counter;

    private void Awake() {
        p_health = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();      
        t_counter = GameObject.FindWithTag("UI").GetComponent<UI_Manager>();
    }

    void Update()
    {
        float playHealth = p_health.playerHealth;
        bool _gameStop = t_counter.gameStop;
            
        if(_gameStop && playHealth < 80)        
            Lose_UI();
        if(playHealth == 100)        
            Win_UI();
        if(_gameStop)
            if(playHealth >= 80f && playHealth < 100f)            
                AlmostWin_UI();            
    }

    void Win_UI()
    {
        winText.gameObject.SetActive(true);
        restart.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(true);  
    }

    void AlmostWin_UI()
    {
        almostWinText.gameObject.SetActive(true);     
        restart.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(true);
    }
    
    void Lose_UI()
    {
        failText.gameObject.SetActive(true);   
        restart.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(true); 
    }
}
