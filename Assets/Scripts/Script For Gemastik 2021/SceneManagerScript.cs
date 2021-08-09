using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SceneManagerScript : MonoBehaviour
{   
    public Transform worldContent;
    public static SceneManagerScript instance;
    private List<string> worldCollections = new List<string>();
    public List<string> WorldCollections {get{return worldCollections;}}
    [HideInInspector] public int numberOfWorlds;
    private void Awake() {
        instance = this;
        numberOfWorlds = worldContent.childCount;

        for(int i = 0 ; i < numberOfWorlds ; i++)
        worldCollections.Add("World_" + (i+1).ToString());
    }

    public void LoadWorld(int index)
    {
        SceneManager.LoadScene("World_" + index.ToString());
    }
}
