using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioClip coin, maleHitSound, maleCorona, femaleHitSound, femaleCorona, updateGold;
    public static AudioClip menuButton, prize, powerUp, newHighScore, playButton, buyWorld;
    private static AudioSource audioSource;
    private static AudioSource musicAudioSource;
    public AudioClip[] musicCollections = new AudioClip[3];
 
    void Start()    
    {
        audioSource = GetComponent<AudioSource>();      
        if(PlayerPrefs.GetInt("Sound", 1) == 0) audioSource.mute = true;
        
        CheckChildAudioSource();
        GetAllClips();  
        MusicToPlay();  
    }

    private void GetAllClips()
    {
        coin = Resources.Load<AudioClip>("Coin");
        maleHitSound = Resources.Load<AudioClip>("ManHit");
        maleCorona = Resources.Load<AudioClip>("ManCorona");
        femaleHitSound = Resources.Load<AudioClip>("FemaleOuch");        
        femaleCorona = Resources.Load<AudioClip>("GirlCorona");
        updateGold = Resources.Load<AudioClip>("updateGold");

        playButton = Resources.Load<AudioClip>("Menu1");        
        menuButton = Resources.Load<AudioClip>("Menu2");        
        prize = Resources.Load<AudioClip>("PrizeFinal");      
        powerUp = Resources.Load<AudioClip>("Prize4");      
        newHighScore = Resources.Load<AudioClip>("NewHighScore");
        buyWorld = Resources.Load<AudioClip>("Buy");
    }

    private void CheckChildAudioSource()
    {
        if(transform.GetChild(0).name == "Null") return;
        else
        {
            musicAudioSource = transform.GetChild(0).GetComponent<AudioSource>();      
            musicAudioSource.loop = true;

            if(PlayerPrefs.GetInt("Music", 1) == 0)
            musicAudioSource.Stop();
        }
    }

    private void MusicToPlay()
    {
        for(int i = 0 ; i < musicCollections.Length ; i++)
        {
            if(UIManager.current.SceneName == "World_" + (i+1))
            musicAudioSource.PlayOneShot(musicCollections[i]);
        }

    }

    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case "MaleHit":
                audioSource.PlayOneShot(maleHitSound);
                CinemachineShake.Instance.ShakeCamera(3f, 0.1f);
                musicAudioSource.Stop();
                break;     
            case "FemaleHit":
                audioSource.PlayOneShot(femaleHitSound);
                CinemachineShake.Instance.ShakeCamera(3f, 0.1f);
                musicAudioSource.Stop();
                break;         
            case "MaleCorona":
                audioSource.PlayOneShot(maleCorona);
                CinemachineShake.Instance.ShakeCamera(3f, 0.1f);
                musicAudioSource.Stop();
                break;     
            case "FemaleCorona":
                audioSource.PlayOneShot(femaleCorona);
                CinemachineShake.Instance.ShakeCamera(3f, 0.1f);
                musicAudioSource.Stop();
                break;         
            case "PlayButton":
                audioSource.PlayOneShot(playButton);
                break;         
            case "Button":
                audioSource.PlayOneShot(menuButton);
                break;         
            case "TakeCoin":
                audioSource.PlayOneShot(coin);
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
            case "Buy":
                audioSource.PlayOneShot(buyWorld);
                break;         
            case "UpdateGold":
                audioSource.PlayOneShot(updateGold);
                break;         
        }
    }
    public static void Vibrate()
    {
        if(PlayerPrefs.GetInt("Vibrate", 1) == 0) return;

        Handheld.Vibrate();
    }
}
