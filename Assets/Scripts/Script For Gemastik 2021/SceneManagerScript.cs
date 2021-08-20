using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class SceneManagerScript : MonoBehaviour
{   
    public static SceneManagerScript instance;
    public Transform worldContent;
    [HideInInspector] public int numberOfWorlds;

    private void Awake() {
        instance = this;
        numberOfWorlds = worldContent.childCount;
    }

    public void LoadWorld(int index)
    {
        SceneManager.LoadScene("World_" + index.ToString());
    }
}
