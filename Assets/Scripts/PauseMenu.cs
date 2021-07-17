using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    private UI_Manager t_counter;    

    private void Awake() {
        t_counter = GameObject.FindWithTag("UI").GetComponent<UI_Manager>();   
    }
    void Update()
    {
        float counter = t_counter.timeCounter;

        if(Input.GetKeyDown(KeyCode.Escape) && counter < 56)
        {
            GameIsPaused = true;
            if(GameIsPaused)
                Pause();
            else
                Resume();
        }

        bool _gameStop = t_counter.gameStop;         
        if(_gameStop)
            this.gameObject.SetActive(false);    
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
