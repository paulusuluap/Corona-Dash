using UnityEngine;
public class FirstPersonController : MonoBehaviour
{
    Rigidbody rb;
    private float walkSpeed = 10f;
    private float turnSpeed = 180f;
    public float WalkSpeed { get{ return walkSpeed; } set{ walkSpeed = value; } }
    public float TurnSpeed { get{ return turnSpeed; } set{ turnSpeed = value; } }
    private float screenWidth, screenHeight;
    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity;
    private PowerUpsController powerUps;
    private Transform planetPos;
    private Renderer myRender;
    public Renderer c_transparent, c_opaque;
    private Color c;

    void Start() {

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
        Quaternion newRotation = Quaternion.Slerp(this.transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        transform.rotation = newRotation;
    }

    private void PlayerInputRotation()
    {   
        int i = 0;
        if(i == Input.touchCount) 
        {
            AnimationManager.current.SetAnim("LookStraight");
            return;
        }
        
        while(i < Input.touchCount)
        {
            //Belok Kiri
            if(Input.GetTouch(i).position.x < screenWidth/2)
            {
                SideMovement(-1f);
                AnimationManager.current.SetAnim("LookLeft");
            }
            //Belok Kanan
            if(Input.GetTouch(i).position.x > screenWidth/2)
            {
                SideMovement(1f);
                AnimationManager.current.SetAnim("LookRight");
            }
            i++;    
        }
    }
    private void SideMovement(float dir)
	{
        float initTurnSpeed = 0f;
        float lerpSpeed = 25f;

        initTurnSpeed = Mathf.Lerp(initTurnSpeed, turnSpeed, lerpSpeed * Time.deltaTime); //make the turn move smooth
		transform.Rotate(Vector3.up * dir * Time.deltaTime * initTurnSpeed, Space.Self);
	}

    private void OnCollisionEnter(Collision obs) {
        if(obs.gameObject.CompareTag("Obstacle"))
        {
            if(ParticleManager.instance.walkDust.isPlaying) ParticleManager.instance.walkDust.Stop();

            AudioManager.Vibrate();
            AudioManager.PlaySound("MaleHit"); //Harus dicek lagi, nanti ada karakter cewe

            AnimationManager.current.SetAnim("DeathType1");
            AnimationManager.current.SetAnim("Die");
            UIManager.current.EndUI();

            this.enabled = false;
        }
    }
}
