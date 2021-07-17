using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    Vector3 moveAmount1, moveAmount2;
    Vector3 smoothMoveVelocity;
    private Rigidbody rb;
    private float counter;      
    private float playHealth;  
    private PlayerHealth p_health;
    private UI_Manager t_counter;

    private void Awake() {
        rb = this.gameObject.GetComponent<Rigidbody>();     
        p_health = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();      
        t_counter = GameObject.FindWithTag("UI").GetComponent<UI_Manager>(); 

        platform = GameManager.PlatformSelection.PC;
    }

    [SerializeField] private GameManager.PlatformSelection platform;

    void Update()
    {
        playHealth = p_health.playerHealth;
        bool _gameStop = t_counter.gameStop; 

        if(platform == GameManager.PlatformSelection.PC)
            PC_Controller();
    }
    
    void PC_Controller()
    {
        Vector3 moveDir1 = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 1f).normalized;
        Vector3 targetMoveAmount1 = moveDir1 * walkSpeed;
        moveAmount1 = Vector3.SmoothDamp(moveAmount1, targetMoveAmount1, ref smoothMoveVelocity, .15f);
    }
    // void MobileController(float joyDir)
    // {
    //     Vector3 moveDir2 = new Vector3(joyDir, 0, 1f).normalized; 
    //     Vector3 targetMoveAmount2 = moveDir2 * walkSpeed;
    //     moveAmount2 = Vector3.SmoothDamp(moveAmount2, targetMoveAmount2, ref smoothMoveVelocity, .15f);  
    // }
    private void FixedUpdate() 
    {
        //PC / Mac Controller
        if(platform == GameManager.PlatformSelection.PC)
            rb.MovePosition(rb.position + transform.TransformDirection(moveAmount1) * Time.fixedDeltaTime);

        //Mobile Controller
        else if(platform == GameManager.PlatformSelection.Mobile)
            rb.MovePosition(rb.position + transform.TransformDirection(moveAmount2) * Time.fixedDeltaTime);
    }
}
    