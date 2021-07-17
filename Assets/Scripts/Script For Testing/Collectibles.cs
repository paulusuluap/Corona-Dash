using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public GameObject coinParent;
    private void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.CompareTag("Player")){
            Destroy();
        }
    }
    private void Destroy () {
        //For Collision component is on the child.
        if(coinParent.activeInHierarchy)
            coinParent.SetActive(false);
    }
}
