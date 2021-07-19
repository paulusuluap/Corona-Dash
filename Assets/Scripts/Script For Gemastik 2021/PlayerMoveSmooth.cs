using UnityEngine;

public class PlayerMoveSmooth : MonoBehaviour
{
    Rigidbody rb;
    public float walkSpeed;    
    public float turnSpeed;  
    private float screenWidth, screenHeight;
    private Vector3 moveAmount;  
    private Vector3 smoothMoveVelocity;
    private Transform planetPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // planetPos = GameObject.FindGameObjectWithTag("Planet").transform;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    private void Movement () {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        // Vector3 direction = new Vector3(0f, 0f, vertical).normalized;
        Vector3 targetMoveAmount = direction * walkSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
        
        if(direction != Vector3.zero) Rotate(direction);

        // rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    private void Update() {
        PlayerInputRotation();
    }
    private void FixedUpdate() {
        Movement(); 
    }

    void Rotate (Vector3 direction) 
    {
        // Vector3 targetDir = (this.transform.position - planet.position).normalized;
        // Quaternion targetRotation = Quaternion.FromToRotation(this.transform.up, targetDir) * this.transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
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
}

