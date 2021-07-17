using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject winSound;
    public GameObject loseSound;
    public GameObject bgMusic;
    private PlayerHealth p_health;
    private UI_Manager t_counter;
    
    public enum PlatformSelection
    {
        Mobile,
        PC
    }
    
    private void Awake() 
    {
        winSound.SetActive(false);
        loseSound.SetActive(false);    

        p_health = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();      
        t_counter = GameObject.FindWithTag("UI").GetComponent<UI_Manager>();
    }

    private void Update() 
    {
        float playHealth = p_health.playerHealth;
        bool _gameStop = t_counter.gameStop;
        
        if(playHealth == 100f)
        {
            bgMusic.SetActive(false);
            winSound.SetActive(true);   
        }
        if(playHealth < 100f && _gameStop)
        {
            bgMusic.SetActive(false);
            loseSound.SetActive(true);
        }        
    }
}
