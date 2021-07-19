using UnityEngine;
using DG.Tweening;

public class Magnet : MonoBehaviour
{
    private PowerUpsController coinMagnet;
    private GravityAttractor planet;
    private void OnEnable() {
        coinMagnet = FindObjectOfType<PowerUpsController>();
        planet = GameObject.FindObjectOfType<GravityAttractor>();
        Attract();
    }

    private void OnDisable() {
        Attract();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player") && !coinMagnet.Magnetized)
        {
            coinMagnet.Magnetized = true;
            this.gameObject.SetActive(false);
        }

        if(other.gameObject.CompareTag("Player") && coinMagnet.Magnetized)
        {
            coinMagnet.MagnetDuration = 10f;
        }
    }    

    private void Attract()
    {
        if(!this.gameObject.activeInHierarchy) return;

        planet.AttractOtherObject(this.transform);
    }
}
