using System.Collections;   
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public static Pooler current;
    public GameObject pooledCoin;
    public GameObject magnet;
    public GameObject corona;
    public bool willGrow = false; // Nonton videonya lagi
    private int pooledCoinAmount = 5;
    private int powerUpsAmount = 1;
    [Range(1,8)] public int pooledCoronaAmount = 4;
    public int PooledCoinAmount {get { return pooledCoinAmount; }}
    public int PowerUpsAmount {get { return powerUpsAmount; }}
    public int PooledCoronaAmount {get { return pooledCoronaAmount; }}
    List<GameObject> pooledCoins = new List<GameObject>();
    List<GameObject> powerUps = new List<GameObject>();
    List<GameObject> pooledCoronas = new List<GameObject>();
    public List<GameObject> PooledCoins {get { return pooledCoins; }}
    public List<GameObject> PowerUps {get { return powerUps; }}
    public List<GameObject> PooledCoronas {get { return pooledCoronas; }}

    private void Awake() {
        current = this;    
    }
    void Start()
    {
        // Tadi list pooledCoins init dihapus dari sini

        for(int i = 0; i < pooledCoinAmount; i++)
        {
            GameObject coin = (GameObject) Instantiate (pooledCoin);
            coin.SetActive(false);
            pooledCoins.Add(coin);
        }

        for(int i = 0; i < powerUpsAmount; i++)
        {
            GameObject powUp = (GameObject) Instantiate (magnet);
            powUp.SetActive(false);
            powerUps.Add(powUp);
        }

        for(int i = 0; i < pooledCoronaAmount; i++)
        {
            GameObject virus = (GameObject) Instantiate (corona);
            virus.SetActive(false);
            pooledCoronas.Add(virus);
        }
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
        // for(int i = 0; i < powerUps.Length ; i++) // Rubah dari List ke Array untuk Magnet, Bonus, Invincible
        for(int i = 0; i < powerUps.Count ; i++)
        {
            if(!powerUps[i].activeInHierarchy)
                return powerUps[i];
        }
        return null;
    }

    public GameObject GetPooledCorona()
    {
        for(int i = 0; i < pooledCoronas.Count ; i++)
        {
            if(!pooledCoronas[i].activeInHierarchy)
                return pooledCoronas[i];
        }

        if(willGrow)
        {
            GameObject obj = (GameObject) Instantiate (corona);
            pooledCoronas.Add(obj);
            return obj;
        }

        return null;
    }
}
