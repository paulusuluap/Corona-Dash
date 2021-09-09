using UnityEngine;
public class FirstPersonController : MonoBehaviour
{
     //static should not be used, when game restart the value still    
    public static FirstPersonController instance;
    Rigidbody rb;
    
    //Initial rotation speed before lerp
    private float turnSpeed = 50f;
    public float TurnSpeed { 
        get => turnSpeed;
        set => turnSpeed = value;
    }
    //Target rotation speed after lerp
    private float finalTurnSpeed = 100f;
    public float FinalTurnSpeed { 
        get => finalTurnSpeed;
        set => finalTurnSpeed = value;
    }

    //Walk or Run speed    
    private float walkSpeed = 10f;
    public float WalkSpeed {
        get => walkSpeed;
        set => walkSpeed = value;
    }
    [Range(0f, 1f)] private float t_lerp = .0f;

    private float screenWidth, screenHeight;
    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity;
    private PowerUpsController powerUps;
    private Transform planetPos;
    private Renderer myRender;
    public Renderer c_transparent, c_opaque;
    private Color c;

    void Start() {
        
        instance = this;
        rb = GetComponent<Rigidbody>();
        myRender = GetComponent<Renderer>();
        powerUps = FindObjectOfType<PowerUpsController>();
        planetPos = FindObjectOfType<GravityAttractor>().transform;

        screenWidth = Screen.width;
        screenHeight = Screen.height;
        
        c = c_transparent.material.GetColor("_BaseColor");
    }

    void Update()
    {
        if(powerUps.IsMagnetized) powerUps.Magnetizing(this);
        if(powerUps.IsInvincible) powerUps.Invulnerable(c_transparent, c_opaque, c);
    }
    private void FixedUpdate() {
        Movement();
        PlayerInputRotation();
    }

    private void Movement () {
        Vector3 direction = new Vector3(0f, 0f, 1f).normalized;
        Vector3 targetMoveAmount = direction * walkSpeed;

        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
        
        if(direction != Vector3.zero) Rotate(direction, planetPos);

        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    private void Rotate (Vector3 moveVector, Transform planet) 
    {   
        //FromtoRotation is for spherical planet, LookRotation for flat surface with different components
        Vector3 targetDir = (this.transform.position - planet.position).normalized;
        Quaternion targetRotation = Quaternion.FromToRotation(this.transform.up, targetDir) * this.transform.rotation;
        Quaternion newRotation = Quaternion.Slerp(this.transform.rotation, targetRotation, finalTurnSpeed * Time.deltaTime);

        transform.rotation = newRotation;
    }

    private void PlayerInputRotation()
    {           
        if(Input.touchCount == 0) 
        {
            turnSpeed = 50f;
            AnimationManager.SetAnim("LookStraight");
            return;
        }
        
        for(int i = 0 ; i < Input.touchCount ; i++)
        {
            if(Input.GetTouch(i).phase == TouchPhase.Stationary || Input.GetTouch(i).phase == TouchPhase.Moved)
            {
                //Belok Kiri
                if(Input.GetTouch(i).position.x < screenWidth/2)
                {
                    MoveSide(-1f);
                    AnimationManager.SetAnim("LookLeft");
                }
                //Belok Kanan
                else if (Input.GetTouch(i).position.x > screenWidth/2)
                {
                    MoveSide(1f);
                    AnimationManager.SetAnim("LookRight");
                }
                //Both
                else
                {
                    MoveSide(0f);
                    turnSpeed = 50f;
                    AnimationManager.SetAnim("LookStraight");
                    return;
                }
            }
            
            if(Input.GetTouch(i).phase == TouchPhase.Ended)
            {
                turnSpeed = 50f;
                AnimationManager.SetAnim("LookStraight");
                return;
            }
        }
    }

    private void MoveSide(float moveDir)
	{    
        turnSpeed = Mathf.Lerp(turnSpeed, finalTurnSpeed, t_lerp); //make the turn move smooth
        t_lerp += 0.5f * Time.deltaTime;
        
        transform.Rotate(Vector3.up * moveDir * Time.deltaTime * turnSpeed, Space.Self);        
	}

    private void OnCollisionEnter(Collision obs) {
        if(obs.gameObject.CompareTag("Obstacle"))
        {
            if(ParticleManager.instance.walkDust.isPlaying) 
            ParticleManager.instance.walkDust.Stop();

            //Male or female sound to play
            if(UIManager.current.SceneName == "World_3")
            AudioManager.PlaySound("FemaleHit");
            else
            AudioManager.PlaySound("MaleHit");

            AnimationManager.SetAnim("DeathType1");
            AnimationManager.SetAnim("Die");
            UIManager.current.EndUI();

            this.enabled = false;
            AudioManager.Vibrate();
        }
    }
}
