using UnityEngine;

public class Magnet : MonoBehaviour
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
        if(other.gameObject.CompareTag("Player") && !powerUps.IsMagnetized)
        {
            powerUps.IsMagnetized = true;
            this.gameObject.SetActive(false);            
            ParticleManager.instance.IdleParticles("PowTaken");
            AudioManager.PlaySound("TakePow");
        }
    }    

    private void Attract()
    {
        if(!this.gameObject.activeInHierarchy) return;

        planet.AttractOtherObject(this.transform);
    }
}
