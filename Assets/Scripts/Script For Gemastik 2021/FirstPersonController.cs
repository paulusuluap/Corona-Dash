using UnityEngine;
public class FirstPersonController : MonoBehaviour
{
    Rigidbody rb;
    private float walkSpeed = 10f;
    public float WalkSpeed { get{ return walkSpeed; } set{ walkSpeed = value; } }
    private float turnSpeed = 180f;
    public float TurnSpeed { get{ return turnSpeed; } set{ turnSpeed = value; } }
    private float screenWidth, screenHeight;
    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity;
    private PowerUpsController powerUps;
    private Transform planetPos;
    private Renderer myRender;
    public Renderer[] characterRender;
    private Color c;

    void Start() {

        rb = GetComponent<Rigidbody>();
        myRender = GetComponent<Renderer>();
        powerUps = FindObjectOfType<PowerUpsController>();
        planetPos = FindObjectOfType<GravityAttractor>().transform;

        screenWidth = Screen.width;
        screenHeight = Screen.height;
        
        c = myRender.material.GetColor("_BaseColor");
    }

    void Update()
    {
        if(powerUps.IsMagnetized) powerUps.Magnetizing(this);
        if(powerUps.IsInvincible) powerUps.Invulnerable(myRender, c);
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
        Quaternion newRotation = Quaternion.Slerp(this.transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        transform.rotation = newRotation;
    }

    private void PlayerInputRotation()
    {   
        int i = 0;
        while(i < Input.touchCount)
        {
            //Belok Kiri
            if(Input.GetTouch(i).position.x < screenWidth/2)
            {
                SideMovement(-1f);
                // AnimationManager.SetAnim("LookLeft");
            }
            //Belok Kanan
            if(Input.GetTouch(i).position.x > screenWidth/2)
            {
                SideMovement(1f);
                // AnimationManager.SetAnim("LookRight");
            }
            i++;    
        }
    }
    private void SideMovement(float dir)
	{
        float initTurnSpeed = 0f;
        float lerpSpeed = 25f;

        initTurnSpeed = Mathf.Lerp(initTurnSpeed, turnSpeed, lerpSpeed * Time.deltaTime);
		transform.Rotate(Vector3.up * dir * Time.deltaTime * initTurnSpeed, Space.Self);
	}
}
