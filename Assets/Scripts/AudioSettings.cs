using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    //For Background Audio
    // [HideInInspector] public AudioSource m_audioSource; 
    // private GravityPull m_Check;
    // private float lastMusicVolume;

    // #region Singleton
    // private static AudioSettings _instance;
    // public static AudioSettings Instance
    // {
    //     get{
    //         if(_instance == null)
    //             Debug.LogWarning("Instance is null");

    //         return _instance;
    //     }
    // }
    // #endregion
    // private void Start() {
    //     _instance = this;

    //     m_audioSource = GetComponent<AudioSource>();
    //     m_audioSource.volume = 1f;

    //     lastMusicVolume = m_audioSource.volume;   

    //     if(GameObject.FindWithTag("FinishLine") != null)
    //         m_Check = GameObject.FindWithTag("FinishLine").GetComponent<GravityPull>();
    //     if(PlayerPrefs.HasKey("MusicVolume"))
    //         UI_Manager.Instance.musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);     
    // }

    // private void Update() {
    //     MusicVolumeAdjustment();                
    //     AudioClipSettings.Instance.SoundVolumeAdjustment();
    // }


    // private void MusicVolumeAdjustment()
    // {        
    //     if(UI_Manager.Instance != null) {
    //         m_audioSource.volume = UI_Manager.Instance.musicSlider.value;
    //         PlayerPrefs.SetFloat("MusicVolume", UI_Manager.Instance.musicSlider.value);
    //     }
    //     else {
    //         m_audioSource.volume = lastMusicVolume;            
    //     }

    //     //Music mute unmute controller
    //     if(UI_Manager.Instance.isMusicOn && m_audioSource.mute)
    //         m_audioSource.mute = false;
    //     else if(!UI_Manager.Instance.isMusicOn && !m_audioSource.mute)
    //         m_audioSource.mute = true;   
         
    //     if(GameManager.Instance.isWin || GameManager.Instance.isDead)
    //         m_audioSource.Stop();
                         
    // }
    
}    