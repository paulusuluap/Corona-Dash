using UnityEngine;

public class Shrinking : MonoBehaviour
{    
    private Vector3 planetScale;
    private PlayerHealth p_health;
    private UI_Manager t_counter;

    private void Awake() {
        p_health = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();      
        t_counter = GameObject.FindWithTag("UI").GetComponent<UI_Manager>();
    }

    void FixedUpdate()
    {
        float playHealth = p_health.playerHealth;
    }
}
        // bool gameStop = t_counter.gameStop; 

        // if(!gameStop)
        // {
        //     transform.localScale -= new Vector3(1, 1, 1) * Time.fixedDeltaTime / 42.5f;
        //     planetScale = transform.localScale;
        // }

        // if(playHealth == 100)
        //     transform.localScale = planetScale;
    
        // if(gameStop)
        //     transform.localScale = planetScale;
