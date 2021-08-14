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
        ParticleManager.instance.SmokeParticles("CoronaOver", this.transform, this.transform);
    }

    private void OnCollisionEnter(Collision col) {   
        if(col.gameObject.CompareTag("Player")) 
        {
            AudioManager.Vibrate();
            col.gameObject.GetComponent<FirstPersonController>().enabled = false;
            Destroy();

            if(UIManager.current.SceneName == "World_3")
            AudioManager.PlaySound("FemaleCorona");
            else
            AudioManager.PlaySound("MaleCorona");
            
            AnimationManager.current.SetAnim("DeathType2");
            AnimationManager.current.SetAnim("Die");
            UIManager.current.EndUI();
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
