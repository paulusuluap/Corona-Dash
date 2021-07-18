using UnityEngine;
using DG.Tweening;

public class CoinTestScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.CompareTag("Player"))
        {
            Destroy();
        }
    }

    private void Destroy () {
        this.gameObject.SetActive(false);
    }
}
