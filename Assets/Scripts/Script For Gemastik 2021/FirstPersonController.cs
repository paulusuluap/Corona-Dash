using System.Collections;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    Rigidbody rb;
    public float rotateSensitivityX = 100f;
    public float walkSpeed = 10f;
    private float verticalLookRotation;
    private float screenWidth, screenHeight;
    private Vector3 moveAmount;
    private Vector3 smoothMoveVelocity;
    private PowerUpsController coinMagnet;

    void Start() {
        rb = GetComponent<Rigidbody>();
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        
        //Jangan Lupa ganti nama script
        coinMagnet = FindObjectOfType<PowerUpsController>();
        
        // Debug.Log("Width : " + screenWidth + " Height : " + screenHeight);
    }

    void Update()
    {
        // horizontal ganti inputan mobile, Jangan lupa dihapus/ disable
        // Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 moveDir = new Vector3(0f, 0, 1f).normalized;
        Vector3 targetMoveAmount = moveDir * walkSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);

        PlayerInputRotation();

        
        if(coinMagnet.Magnetized)
            coinMagnet.Magnetizing(this);
    }
    private void FixedUpdate() {
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    private void SideMovement(float magnitude)
	{
		transform.Rotate(Vector3.up * magnitude * Time.deltaTime * rotateSensitivityX, Space.Self);
	}

    private void PlayerInputRotation()
    {
        int i = 0;
        while(i < Input.touchCount)
        {
            if(Input.GetTouch(i).position.x > screenWidth/2)
                SideMovement(1f);
            
            if(Input.GetTouch(i).position.x < screenWidth/2)
                SideMovement(-1f);
            
            if(Input.GetTouch(i).position.x > screenWidth/2 && Input.GetTouch(i).position.x < screenWidth/2)
                return;
            i++;    
        }
    }

    // private void OnDrawGizmos() 
    // {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(this.transform.position, 10f);
    // }
}
