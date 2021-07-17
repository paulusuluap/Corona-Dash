using UnityEngine;

public class AnimationController : MonoBehaviour
{   private Animator anim;
    public GameObject cm_1;
    public GameObject cm_2;    
    private float playHealth;
    private PlayerHealth p_health;
    private UI_Manager t_counter;
    private bool gameStop;
    private float counter;

    private void Start() 
    {
        anim = GameObject.FindWithTag("Anim").GetComponent<Animator>();  
        p_health = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();      
        t_counter = GameObject.FindWithTag("UI").GetComponent<UI_Manager>();
    }
    void Update()
    {
        playHealth = p_health.playerHealth;
        gameStop = t_counter.gameStop == true;
        
        if(playHealth < 80f && gameStop)
        {
            anim.SetBool("Death_b", true);
            anim.SetInteger("DeathType_int", 1);       
            anim.SetFloat("Speed_f", 0.0f);     
        }
        
        if(gameStop)
            if(playHealth >= 80 && playHealth < 100)
            {
                anim.SetFloat("Speed_f", 0.0f);
                anim.SetInteger("Animation_int", 9);
            }

        if(playHealth == 100f)
        {
            anim.SetTrigger("Jump_trig");     
            anim.SetFloat("Speed_f", 0.0f);       
        }

        //Cinemachine setter                
        if(gameStop || playHealth == 100f)                
        {
            cm_1.gameObject.SetActive(false);
            cm_2.gameObject.SetActive(true);        
        }
    }
}
