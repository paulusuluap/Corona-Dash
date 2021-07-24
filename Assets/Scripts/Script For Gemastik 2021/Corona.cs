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
        ParticleManager.instance.SmokeParticles("CoronaOver", this.transform, this.transform);
    }

    private void OnCollisionEnter(Collision col) {   
        if(col.gameObject.CompareTag("Player")) 
        {
            col.gameObject.GetComponent<FirstPersonController>().enabled = false;
            Destroy();

            AnimationManager.current.SetAnim("DeathType2");
            AnimationManager.current.SetAnim("Die");

        if(col.gameObject.CompareTag("Obstacle")) gameObject.SetActive(false);
            
            //Panggil game over
            // - Cinemachine camera rotate
            // - Score appear
            // - Restart button appear
            // - Pulang ke home       
        }
    }

    private void Destroy() {
        ParticleManager.instance.SmokeParticles("PeopleDie", this.transform, this.transform);
        this.gameObject.SetActive(false);
    }

    public void CoronaMoveToPlayer(FirstPersonController player, float coronaSpeed)
    {
        if(!this.gameObject.activeInHierarchy || !player.enabled) return;

        Vector3 chasePlayer = Vector3.MoveTowards(this.transform.position, player.transform.position, coronaSpeed * Time.deltaTime);
        rb.MovePosition(chasePlayer);
    }

    private void Attract()
    {
        if(!this.gameObject.activeInHierarchy) return;

        planet.AttractOtherObject(this.transform);
    }

    // private void OnDrawGizmos() {
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireCube(this.transform.position ,new Vector3 (15.0f, 15.0f, 15.0f));
    // }
}
