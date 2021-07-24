using UnityEngine;

public class Collectibles : MonoBehaviour
{
    public GameObject coinParent;
    private void OnTriggerEnter(Collider col) 
    {
        if(col.gameObject.CompareTag("Player")) Destroy();
        else if(!col.gameObject.CompareTag("Player")) Destroy();
    }
    private void Destroy () {
        coinParent.SetActive(false);
    }
}
