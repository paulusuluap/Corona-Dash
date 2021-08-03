using UnityEngine;

public class Invincible : MonoBehaviour
{
    private PowerUpsController powerUps;
    private GravityAttractor planet;

    private void OnEnable() 
    {
        powerUps = FindObjectOfType<PowerUpsController>();
        planet = GameObject.FindObjectOfType<GravityAttractor>();
        Attract();
    }

    private void OnDisable() {
        Attract();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player") && !powerUps.IsInvincible)
        {
            powerUps.IsInvincible = true;
            this.gameObject.SetActive(false);
            AudioManager.PlaySound("TakePow");
            ParticleManager.instance.IdleParticles("PowTaken");
        }
    }    

    private void Attract()
    {
        if(!this.gameObject.activeInHierarchy) return;

        planet.AttractOtherObject(this.transform);
    }
}
