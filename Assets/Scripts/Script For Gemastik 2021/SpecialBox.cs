using UnityEngine;

public class SpecialBox : MonoBehaviour
{
    private int prize;
    private int[] prizePool = 
        {5, 8, 10, 13, 15, 17, 20, 23, 25, 26, 30, 32, 
        35, 39, 40, 41, 45, 50, 52, 55, 56, 58, 60, 62, 
        65, 68, 70, 72, 74, 75, 77, 80, 81, 83, 85, 87,
        88, 90, 95, 100, 135, 150, 200, 500, 1000 };
    private GravityAttractor planet;

    private void OnEnable() 
    {
        planet = GameObject.FindObjectOfType<GravityAttractor>();
        prize = Random.Range(0, prizePool.Length);
        // Debug.Log("Coin Prize : " + (int)prizePool[prize]); //Jangan lupa dihapus   
        Attract();
    }

    private void OnDisable() 
    {
        Attract();
    }

    private void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.CompareTag("Player"))
        {            
            //Coin Collected += prizePool[prize];
            this.gameObject.SetActive(false);
        }
    }

    private void Attract()
    {
        if(!this.gameObject.activeInHierarchy) return;

        planet.AttractOtherObject(this.transform);
    }
}
