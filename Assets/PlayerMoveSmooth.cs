using UnityEngine;

public class PlayerMoveSmooth : MonoBehaviour
{
    CharacterController characterController;
    private Vector3 velocity;
    private float gravity = -9.81f;
    public float turnSpeed = 5f;
    public float walkSpeed = 3f;    

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        velocity.y += gravity * Time.deltaTime;
        
        Move();
    }

    void Move () {
        float horizontal = Input.GetAxisRaw("Horizontal") * walkSpeed * Time.deltaTime;
        float vertical = Input.GetAxisRaw("Vertical") * walkSpeed * Time.deltaTime;

        Vector3 moveVector = new Vector3(horizontal, 0f, vertical);
        // Vector3 moveVector = new Vector3(horizontal, 0f, Time.deltaTime);

        characterController.Move(moveVector + velocity);

        if(moveVector != Vector3.zero)
            Rotate(moveVector);

    }

    void Rotate (Vector3 moveVector) {
        Quaternion targetRotation = Quaternion.LookRotation(moveVector, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(this.transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        transform.rotation = newRotation;
    }

    public float GetCurrentSpeed()
    {
        return 3f;
    }
}

