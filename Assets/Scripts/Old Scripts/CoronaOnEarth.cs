using UnityEngine;

public class CoronaOnEarth : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        var Player = other.gameObject.CompareTag("Player");

        if(Player)                    
        {
            gameObject.SetActive(false);
            CinemachineShake.Instance.ShakeCamera(4f, 0.2f);
            
            if(PlayerPrefs.GetInt("CharacterSelected") == 3)
                AudioManager.PlaySound("FemaleOuch");
            else        
                AudioManager.PlaySound("Ouch");  
        }
    }
}
