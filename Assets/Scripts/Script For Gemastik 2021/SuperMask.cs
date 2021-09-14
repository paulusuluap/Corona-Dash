using UnityEngine;

public class SuperMask : MonoBehaviour
{
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
        if(other.gameObject.CompareTag("Player") && !PowerUpsController.Instance.IsMaskActive)
        {            
            PowerUpsController.Instance.ActivateMask();
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