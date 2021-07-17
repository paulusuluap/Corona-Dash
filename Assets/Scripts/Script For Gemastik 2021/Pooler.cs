using System.Collections;   
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public static Pooler current;
    public GameObject pooledCoin;
    public GameObject magnet;
    [Range(1,7)] public int pooledCoinAmount = 7;
    [Range(1,3)] public int powerUpsAmount = 1;
    List<GameObject> pooledCoins = new List<GameObject>();
    List<GameObject> powerUps = new List<GameObject>();

    private void Awake() {
        current = this;    
    }
    void Start()
    {
        // Tadi list pooledCoins init dihapus dari sini

        for(int i = 0; i < pooledCoinAmount; i++)
        {
            GameObject obj = (GameObject) Instantiate (pooledCoin);
            obj.SetActive(false);
            pooledCoins.Add(obj);
        }

        for(int i = 0; i < 1; i++)
        {
            GameObject obj = (GameObject) Instantiate (magnet);
            obj.SetActive(false);
            powerUps.Add(obj);
        }

        // foreach(GameObject powerUp in powerUps)
        // {
        //     if(powerUp.activeInHierarchy)
        //         powerUp.SetActive(false);
        // }
    }

    public GameObject GetPooledCoins()
    {
        for(int i = 0; i < pooledCoins.Count ; i++)
        {
            if(!pooledCoins[i].activeInHierarchy)
                return pooledCoins[i];
        }
        return null;
    }

    public GameObject GetPooledPowerUps()
    {
        // for(int i = 0; i < powerUps.Length ; i++)
        for(int i = 0; i < powerUps.Count ; i++)
        {
            if(!powerUps[i].activeInHierarchy)
                return powerUps[i];
        }
        return null;
    }

    // public GameObject GetPooledEnemies()
    // {
    //     for(int i = 0; i < pooledEnemies.Count ; i++)
    //     {
    //         if(!pooledEnemies[i].activeInHierarchy)
    //             return pooledEnemies[i];
    //     }

    //     if(willGrow)
    //     {
    //         GameObject obj = (GameObject) Instantiate (pooledEnemy);
    //         pooledEnemies.Add(obj);
    //         return obj;
    //     }

    //     return null;
    // }
}
