using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        var player = other.gameObject.CompareTag("Player");
        if(player)
        {               
            if(PlayerPrefs.GetInt("CharacterSelected") == 3)
                AudioManager.PlaySound("FemaleOuch");
            else        
                AudioManager.PlaySound("Ouch");  
                
            CinemachineShake.Instance.ShakeCamera(3f, 0.1f);
        }
    }
}
