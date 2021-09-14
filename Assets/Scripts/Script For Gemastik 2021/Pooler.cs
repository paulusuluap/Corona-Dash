using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public static Pooler current;
    public bool coronaStage1 = false;
    public bool coronaStage2 = false;
    public GameObject pooledCoin;
    
    [Header("Power Ups")]
    [SerializeField] private GameObject magnetPrefab;
    [SerializeField] private GameObject maskPrefab;
    [SerializeField] private GameObject invincibleStarPrefab;
    [SerializeField] private GameObject multiplierPrefab;

    [Header("Prize")]public GameObject prizeBox;
    [Header("Corona")]public GameObject pooledCorona;
    private int pooledCoinAmount = 6; // Coin amount controller
    private int powerUpsAmount = 4; // Pow Up amount controller
    private int prizeBoxAmount = 1; 
    private int pooledCoronaAmount = 4; // Corona amount controller
    private Vector3 powerUpSpawnLocation = new Vector3 (-7.186849f, -8.279894f, -12.61536f);
    private Vector3 prizeSpawnLocation = new Vector3 (0f, 16.21f, 0f);

    public int PooledCoinAmount {get => pooledCoinAmount; }
    public int PowerUpsAmount {get => powerUpsAmount; }
    public int PrizeSpawnAmount {get => prizeBoxAmount; }
    public Vector3 PrizeBoxLocation {get => prizeSpawnLocation; }
    public Vector3 PowerUpSpawnLocation {get => powerUpSpawnLocation; }
    public int PooledCoronaAmount {get => pooledCoronaAmount; }
    
    List<GameObject> pooledCoins = new List<GameObject>();
    List<GameObject> powerUps = new List<GameObject>();
    List<GameObject> prizeBoxes = new List<GameObject>();
    List<GameObject> pooledCoronas = new List<GameObject>();
    public Transform[] coronaPositions = new Transform[10];

    public List<GameObject> PowerUps {get => powerUps; }
    public List<GameObject> PrizeBoxes {get => prizeBoxes; }
    public List<GameObject> PooledCoronas {get => pooledCoronas; }

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

        if(coronaStage1)
        {
            GameObject obj = (GameObject) Instantiate (pooledCorona);
            pooledCoronas.Add(obj);
            return obj;
        }

        if(coronaStage2)
        {
            GameObject obj = (GameObject) Instantiate (pooledCorona);
            pooledCoronas.Add(obj);
            return obj;
        }

        return null;
    }

    private void PowerUpsInstantiation()
    {
        GameObject m_Magnet = (GameObject) Instantiate (magnetPrefab);
        GameObject m_Mask = (GameObject) Instantiate (maskPrefab);
        GameObject m_Invincible = (GameObject) Instantiate (invincibleStarPrefab);
        GameObject m_Multiplier = (GameObject) Instantiate (multiplierPrefab);

        powerUps.Add(m_Magnet);
        powerUps.Add(m_Mask);
        powerUps.Add(m_Invincible);
        powerUps.Add(m_Multiplier);
        
        for(int i = 0; i < powerUpsAmount; i++) 
        powerUps[i].SetActive(false);
    }
}
