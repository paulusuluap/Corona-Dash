using UnityEngine;

public class Invincible : MonoBehaviour
{
    // private PowerUpsController powerUps;
    private GravityAttractor planet;

    private void OnEnable() 
    {
        // powerUps = FindObjectOfType<PowerUpsController>();
        planet = GameObject.FindObjectOfType<GravityAttractor>();
        Attract();
    }

    private void OnDisable() {
        Attract();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player") && !PowerUpsController.Instance.IsInvincibleActive)
        {
            PowerUpsController.Instance.ActivateInvincibility();
            AudioManager.PlaySound("TakePow");
            ParticleManager.instance.IdleParticles("PowTaken");
            
            this.gameObject.SetActive(false);
        }
    }    

    private void Attract()
    {
        if(!this.gameObject.activeInHierarchy) return;

        planet.AttractOtherObject(this.transform);
    }
}
