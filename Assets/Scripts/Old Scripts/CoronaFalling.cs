using UnityEngine;

public class CoronaFalling : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    private Vector3 gravityPos;
    private Vector3 direction;
    
    private void Start() 
    {        
        rb = GetComponent<Rigidbody>();
        gravityPos = GameObject.FindGameObjectWithTag("Planet").GetComponent<Transform>().position;
    }

    private void FixedUpdate() 
    {
        rb.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    private void Update() 
    {
        direction = gravityPos - transform.position;             
        transform.rotation = Quaternion.LookRotation(direction);     
        Debug.DrawLine(this.transform.position, gravityPos, Color.green);   
    }
    
    private void OnTriggerEnter(Collider other) 
    {
        var Earth = other.gameObject.CompareTag("Planet");
        var Player = other.gameObject.CompareTag("Player");
        bool _gameStop = FindObjectOfType<UI_Manager>().gameStop;
        
        if(Earth)                    
            gameObject.SetActive(false);                     
        
        
        if(Player && !_gameStop)
        {
            gameObject.SetActive(false);
            CinemachineShake.Instance.ShakeCamera(4f, 0.1f);
                        
            if(PlayerPrefs.GetInt("CharacterSelected") == 3)
                AudioManager.PlaySound("FemaleOuch");
            else        
                AudioManager.PlaySound("Ouch");  
        }else if(Player && _gameStop)
        {
            gameObject.SetActive(false);
            CinemachineShake.Instance.ShakeCamera(0f, 0f);
        }
    }
}
