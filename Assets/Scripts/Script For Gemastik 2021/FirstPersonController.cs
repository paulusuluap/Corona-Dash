using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    Rigidbody rb;
    private float walkSpeed = 10f;
    public float WalkSpeed { get{ return walkSpeed; } set{ walkSpeed = value; } }
    private float turnSpeed = 100f;
    public float TurnSpeed { get{ return turnSpeed; } set{ turnSpeed = value; } }
    private float screenWidth, screenHeight;
    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity;
    private PowerUpsController coinMagnet;
    private Transform planetPos;

    void Start() {
        rb = GetComponent<Rigidbody>();
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        
        coinMagnet = FindObjectOfType<PowerUpsController>();
        planetPos = FindObjectOfType<GravityAttractor>().transform;
        
        // Debug.Log("Width : " + screenWidth + " Height : " + screenHeight);
    }

    void Update()
    {
        PlayerInputRotation();

        if(coinMagnet.Magnetized) coinMagnet.Magnetizing(this);
    }
    private void FixedUpdate() {
        Movement();
    }

    private void Movement () {
        // Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 direction = new Vector3(0f, 0f, 1f).normalized;
        Vector3 targetMoveAmount = direction * walkSpeed;

        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
        
        if(direction != Vector3.zero) Rotate(direction, planetPos);

        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    //Kayanya harus dihapus moveVector, gk dipakek
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
                SideMovement(-1f);
            
            //Belok Kanan
            if(Input.GetTouch(i).position.x > screenWidth/2)
                SideMovement(1f);
                
            i++;    
        }
    }
    private void SideMovement(float dir)
	{
		transform.Rotate(Vector3.up * dir * Time.deltaTime * turnSpeed, Space.Self);
	}


    // private void OnDrawGizmos() 
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(this.transform.position, 10f);
    // }
}
