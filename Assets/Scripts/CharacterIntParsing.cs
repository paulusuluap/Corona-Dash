using UnityEngine;

public class CharacterIntParsing : MonoBehaviour
{
    int playerIndex;
    private void Start() 
    {
        playerIndex = PlayerPrefs.GetInt("CharacterSelected");   
        transform.GetChild(playerIndex).gameObject.SetActive(true);
    }
}
