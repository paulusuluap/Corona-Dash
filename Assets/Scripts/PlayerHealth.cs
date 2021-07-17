using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    public float playerHealth;
    private float storeHealth;
    public float maxPlayerHealth;
    private ParticleSystem particleSys;
    private UI_Manager t_counter;
    private float counter;
    private bool _gameStop;
    void Start()
    {
        playerHealth = 0;
        maxPlayerHealth = 100;

        t_counter = GameObject.FindWithTag("UI").GetComponent<UI_Manager>();
    }
    
    void Update()
    {   
        counter = t_counter.timeCounter;
        _gameStop = t_counter.gameStop;

        storeHealth = playerHealth;
        playerHealth += Time.deltaTime * 1.15f;
        healthBar.value = playerHealth;             

        if(playerHealth >= maxPlayerHealth)
            playerHealth = maxPlayerHealth;
            
        if(playerHealth < maxPlayerHealth && _gameStop)
            playerHealth = storeHealth;        
    }    
    
    private void OnTriggerEnter(Collider hitInfo) {
        bool corona = hitInfo.gameObject.CompareTag("Corona");
        bool greenHealth = hitInfo.gameObject.CompareTag("Health");
        bool obstacle = hitInfo.gameObject.CompareTag("Obstacle");

        if(corona && !_gameStop)
            playerHealth -= 3;
        else if(greenHealth && !_gameStop)
            playerHealth += 3;
        else if(obstacle && !_gameStop)
            playerHealth -= 1.5f;
        else
            playerHealth = storeHealth;
    }
}
