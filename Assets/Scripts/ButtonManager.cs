using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    private CanvasGroup menuOpacity;

    private void Awake() {
        menuOpacity = GameObject.FindWithTag("UIFeatures").GetComponent<CanvasGroup>();
    }
    public void StartGame()
    {                
        LoadPlay();        
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void CharacterSelection()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadPlay()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }
    

    IEnumerator LoadLevel(int sceneIndex)
    {        
        transition.SetTrigger("Start");
        menuOpacity.alpha = 0;

        yield return new WaitForSeconds(transitionTime);   
    
        SceneManager.LoadScene(sceneIndex);
    }

}
