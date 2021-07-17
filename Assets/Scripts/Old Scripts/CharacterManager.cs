using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    private int currentChar;

    private void Awake() 
    {
        SelectChar(0);
    }

    private void SelectChar(int _index)
    {        
        //Enabling / Disabling buttons
        leftButton.interactable = (_index != 0);
        rightButton.interactable = (_index != transform.childCount - 1);

        //Activating indexed GameObject
        for(int i = 0; i < transform.childCount; i++)        
            transform.GetChild(i).gameObject.SetActive(i == _index);
    }

    public void ChangeCha(int _change)
    {
        currentChar += _change;
        SelectChar(currentChar);
    }

    public void MainMenu()
    {
        PlayerPrefs.SetInt("CharacterSelected", currentChar);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
