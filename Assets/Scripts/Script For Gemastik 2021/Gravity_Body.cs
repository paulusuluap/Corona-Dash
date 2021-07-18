using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Gravity_Body : MonoBehaviour
{
    private Rigidbody rb;
    private GravityAttractor planet;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation; 
    }
    private void FixedUpdate() {
        planet.AttractPlayer(this.transform);
    }
}
