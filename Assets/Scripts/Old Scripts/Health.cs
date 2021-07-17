using UnityEngine;

public class Health : MonoBehaviour
{
    private Vector3 gravityPos;
    private Vector3 direction;
    private Quaternion healthRot;
    
    void Start()
    {
        gravityPos = GameObject.FindWithTag("Planet").GetComponent<Transform>().position;
        healthRot = transform.rotation;
    }

    void Update()
    {
        direction = gravityPos - transform.position;           
        healthRot = Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, 0);
        transform.rotation = healthRot;
        Debug.DrawLine(gravityPos, transform.position, Color.green);
    }

    private void OnTriggerEnter(Collider player) {
        if(player.gameObject.CompareTag("Player"))
        {
            AudioManager.PlaySound("HealthCollide");
            Destroy(this.gameObject);
        }
            
    }
}
