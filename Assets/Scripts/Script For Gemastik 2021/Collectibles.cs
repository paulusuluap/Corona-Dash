using UnityEngine;
using System;

public class Collectibles : MonoBehaviour
{
    public static event Action CoinPickedUp = delegate{};
    public GameObject coinParent;
    private void OnTriggerEnter(Collider col) 
    {
        CoinPickedUp();
        
        AudioManager.PlaySound("TakeCoin");
        if(col.gameObject.CompareTag("Player")) Destroy();
    }
    private void Destroy () {
        coinParent.SetActive(false);
    }
}
