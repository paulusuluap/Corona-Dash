using System.Collections;   
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public static Pooler current;
    public bool willGrow = false;
    public GameObject pooledCoin;
    [Header("Power Ups")]
    public GameObject magnet;
    // public GameObject star;
    [Header("Prize")]public GameObject prizeBox;
    [Header("Corona")]public GameObject pooledCorona;
    private int pooledCoinAmount = 5;
    private int powerUpsAmount = 1; //Nanti diganti 2 ya hehehe
    private int prizeBoxAmount = 1;
    private int pooledCoronaAmount = 4;

    public int PooledCoinAmount {get { return pooledCoinAmount; }}
    public int PowerUpsAmount {get { return powerUpsAmount; }}
    public int PrizeBoxAmount {get { return prizeBoxAmount; }}
    public int PooledCoronaAmount {get { return pooledCoronaAmount; }}
    
    List<GameObject> pooledCoins = new List<GameObject>();
    List<GameObject> powerUps = new List<GameObject>();
    List<GameObject> prizeBoxes = new List<GameObject>();
    List<GameObject> pooledCoronas = new List<GameObject>();

    public List<GameObject> PowerUps {get { return powerUps; }}
    public List<GameObject> PrizeBoxes {get { return prizeBoxes; }}
    public List<GameObject> PooledCoronas {get { return pooledCoronas; }}

    private void Awake() {
        current = this;    
    }
    void Start()
    {
        //Coins Instantiation
        for(int i = 0; i < pooledCoinAmount; i++)
        {
            GameObject coin = (GameObject) Instantiate (pooledCoin);
            coin.SetActive(false);
            pooledCoins.Add(coin);
        }

        //Power Ups Instantiation
        PowerUpsInstantiation();

        //Prize Instantiation
        for(int i = 0; i < prizeBoxAmount; i++)
        {
            GameObject prize = (GameObject) Instantiate (prizeBox);
            prize.SetActive(false);
            prizeBoxes.Add(prize);
        }
        
        //Virus Instantiation
        for(int i = 0; i < pooledCoronaAmount; i++)
        {
            GameObject virus = (GameObject) Instantiate (pooledCorona);
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

    public GameObject GetPrize()
    {
        for(int i = 0; i < prizeBoxes.Count ; i++)
        {
            if(!prizeBoxes[i].activeInHierarchy)
                return prizeBoxes[i];
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
            GameObject obj = (GameObject) Instantiate (pooledCorona);
            pooledCoronas.Add(obj);
            return obj;
        }

        return null;
    }

    private void PowerUpsInstantiation()
    {
        GameObject m_magnet = (GameObject) Instantiate (magnet);
        // GameObject m_invincible = (GameObject) Instantiate (star);

        powerUps.Add(m_magnet);
        // powerUps.Add(m_invincible);
        
        for(int i = 0; i < powerUpsAmount; i++) powerUps[i].SetActive(false);
    }
}
