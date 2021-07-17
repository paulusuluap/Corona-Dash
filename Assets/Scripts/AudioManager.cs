using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip healthHitSound, damagedSound, female_damagedSound; 
    private static AudioSource audioSource;
 
    void Start()    
    {
        healthHitSound = Resources.Load<AudioClip>("HealthCollide");
        damagedSound = Resources.Load<AudioClip>("Ouch");
        female_damagedSound = Resources.Load<AudioClip>("FemaleOuch");        
    
        audioSource = GetComponent<AudioSource>();      
    }

    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case "HealthCollide":
                audioSource.PlayOneShot(healthHitSound);
                break;
            case "Ouch":
                audioSource.PlayOneShot(damagedSound);
                break;     
            case "FemaleOuch":
                audioSource.PlayOneShot(female_damagedSound);
                break;         
        }
    }
}
