using UnityEngine;

public class AudioClipSettings : MonoBehaviour
{    
    // [HideInInspector] public AudioSource m_audioSource;
    // public static AudioClip die, win, play, socMed, setMenu;
    // private float lastSoundVolume;

    // #region Singleton
    // private static AudioClipSettings _instance;
    // public static AudioClipSettings Instance
    // {
    //     get{
    //         if(_instance == null)
    //             Debug.LogWarning("Instance is null");

    //         return _instance;
    //     }
    // }
    // #endregion
    
    // private void Awake() {
    //     m_audioSource = GetComponent<AudioSource>();

    //     _instance = this;
    //     die = Resources.Load<AudioClip>("DieSound");
    //     win = Resources.Load<AudioClip>("WinSound");
    //     play = Resources.Load<AudioClip>("PlaySound");
    //     socMed = Resources.Load<AudioClip>("Socmed");
    //     setMenu = Resources.Load<AudioClip>("Setting");

    //     lastSoundVolume = m_audioSource.volume;
        
    //     //Baru nambah yang ini
    //     if(PlayerPrefs.HasKey("SoundVolume"))
    //         UI_Manager.Instance.soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1f);
    // }

    // public void PlaySound(string clip)
    // {
    //     switch(clip)
    //     {
    //         case "die":
    //             m_audioSource.PlayOneShot(die);
    //             break;    
    //         case "win":
    //             m_audioSource.PlayOneShot(win);
    //             break;
    //         case "play":
    //             m_audioSource.PlayOneShot(play);
    //             break;
    //         case "menuButton":
    //             m_audioSource.PlayOneShot(socMed);
    //             break;
    //         case "setting":
    //             m_audioSource.PlayOneShot(setMenu);
    //             break;
    //     }
    // }

    // public void SoundVolumeAdjustment()
    // {
    //     if(UI_Manager.Instance != null)
    //     {            
    //         m_audioSource.volume = UI_Manager.Instance.soundSlider.value;
    //         //Baru nambah yang ini
    //         PlayerPrefs.SetFloat("SoundVolume", UI_Manager.Instance.soundSlider.value);
    //     }
    //     else
    //         m_audioSource.volume = lastSoundVolume;        

    //     if(UI_Manager.Instance.isSoundOn && m_audioSource.mute)
    //         m_audioSource.mute = false;
    //     else if(!UI_Manager.Instance.isSoundOn && !m_audioSource.mute)
    //         m_audioSource.mute = true;   
    // }    
}
