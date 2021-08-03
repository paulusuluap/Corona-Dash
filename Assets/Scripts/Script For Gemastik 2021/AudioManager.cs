using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioClip coin, maleHitSound, maleCorona, femaleHitSound, femaleCorona, menuButton, prize, powerUp, newHighScore; 
    private static AudioSource audioSource;
    private static AudioSource musicAudioSource;
 
    void Start()    
    {
        audioSource = GetComponent<AudioSource>();      
        musicAudioSource = transform.GetChild(0).GetComponent<AudioSource>();      
        musicAudioSource.loop = true;

        coin = Resources.Load<AudioClip>("Coin");
        maleHitSound = Resources.Load<AudioClip>("ManHit");
        maleCorona = Resources.Load<AudioClip>("ManCorona");
        femaleHitSound = Resources.Load<AudioClip>("FemaleOuch");        
        femaleCorona = Resources.Load<AudioClip>("GirlCorona");        
        menuButton = Resources.Load<AudioClip>("Menu2");        
        prize = Resources.Load<AudioClip>("PrizeFinal");      
        powerUp = Resources.Load<AudioClip>("Prize4");      
        newHighScore = Resources.Load<AudioClip>("NewHighScore");      
    }

    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case "TakeCoin":
                audioSource.PlayOneShot(coin);
                break;
            case "MaleHit":
                audioSource.PlayOneShot(maleHitSound);
                CinemachineShake.Instance.ShakeCamera(3f, 0.1f);
                musicAudioSource.Stop();
                break;     
            case "MaleCorona":
                audioSource.PlayOneShot(maleCorona);
                CinemachineShake.Instance.ShakeCamera(3f, 0.1f);
                musicAudioSource.Stop();
                break;     
            case "FemaleHit":
                audioSource.PlayOneShot(femaleHitSound);
                CinemachineShake.Instance.ShakeCamera(3f, 0.1f);
                musicAudioSource.Stop();
                break;         
            case "FemaleCorona":
                audioSource.PlayOneShot(femaleCorona);
                CinemachineShake.Instance.ShakeCamera(3f, 0.1f);
                musicAudioSource.Stop();
                break;         
            case "Button":
                audioSource.PlayOneShot(menuButton);
                break;         
            case "TakePrize":
                audioSource.PlayOneShot(prize);
                break;         
            case "TakePow":
                audioSource.PlayOneShot(powerUp);
                break;         
            case "NewHighScore":
                audioSource.PlayOneShot(newHighScore);
                break;         
        }
    }
}
