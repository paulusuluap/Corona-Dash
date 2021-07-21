using UnityEngine;

public class Corona : MonoBehaviour
{
    private GravityAttractor planet;
    private Rigidbody rb;
    
    private void OnEnable() {   
        rb = this.gameObject.GetComponent<Rigidbody>();
        planet = GameObject.FindObjectOfType<GravityAttractor>();
        Attract();
    }

    private void OnDisable() {
        Attract();
    }

    private void OnCollisionEnter(Collision col) {   
        if(col.gameObject.CompareTag("Player")) 
        {
            Destroy();     
            col.gameObject.GetComponent<FirstPersonController>().enabled = false;
            AnimationManager.SetAnim("Die");
            //Panggil game over
            // - Cinemachine camera rotate
            // - Score appear
            // - Restart button appear
            // - Pulang ke home       
        }
    }

    private void Destroy() {
        this.gameObject.SetActive(false);
    }

    public void CoronaMoveToPlayer(FirstPersonController player, float coronaSpeed)
    {
        if(!this.gameObject.activeInHierarchy) return;

        Vector3 chasePlayer = Vector3.MoveTowards(this.transform.position, player.transform.position, coronaSpeed * Time.deltaTime);
        rb.MovePosition(chasePlayer);
    }

    private void Attract()
    {
        if(!this.gameObject.activeInHierarchy) return;

        planet.AttractOtherObject(this.transform);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position ,new Vector3 (15.0f, 15.0f, 15.0f));
    }
}
