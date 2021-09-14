using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMultiplier : MonoBehaviour
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
        if(other.gameObject.CompareTag("Player") && !PowerUpsController.Instance.IsMultiplierActive)
        {
            PowerUpsController.Instance.ActivateMultiplier();
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
