using UnityEngine;

public class Magnet : MonoBehaviour
{
    private PowerUpsController coinMagnet;
    private void OnEnable() {
        coinMagnet = FindObjectOfType<PowerUpsController>();
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
}
