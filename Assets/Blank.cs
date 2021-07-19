using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blank : MonoBehaviour
{
    public static Blank current;
    public GameObject corona;
    public bool willGrow = false; // Nonton videonya lagi
    [Range(1,7)] public int pooledCoronaAmount = 5;
    List<GameObject> pooledCorona = new List<GameObject>();
    public List<GameObject> PooledCorona {get { return pooledCorona; }}

    private void Awake() {
        current = this;    
    }
    void Start()
    {
        // Tadi list pooledCoins init dihapus dari sin

        for(int i = 0; i < pooledCoronaAmount; i++)
        {
            GameObject virus = (GameObject) Instantiate (corona);
            virus.SetActive(false);
            pooledCorona.Add(virus);
        }
    }

    public GameObject GetPooledCorona()
    {
        for(int i = 0; i < pooledCorona.Count ; i++)
        {
            if(!pooledCorona[i].activeInHierarchy)
                return pooledCorona[i];
        }

        if(willGrow)
        {
            GameObject obj = (GameObject) Instantiate (corona);
            pooledCorona.Add(obj);
            return obj;
        }

        return null;
    }
}
