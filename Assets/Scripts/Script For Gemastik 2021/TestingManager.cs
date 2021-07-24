using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TestingManager : MonoBehaviour
{
    [SerializeField]
    private float offset = 8.5f;
    private float planetRadius;
    private float objectPosOnPlanet;
    private float randomPick;
    GravityAttractor planet;
    FirstPersonController player;
    Vector3[] randomPos;


    void Start()
    {
        DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(2000, 100);

        player = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();

        planetRadius = this.transform.localScale.x/2;
        objectPosOnPlanet = planetRadius + offset;
        randomPos = new Vector3[4];

        float prizeInitalTime = Random.Range(30f, 60f);

        InvokeRepeating("PrizeSpawning", prizeInitalTime, 300f);
        InvokeRepeating("CoinSpawning", 2f, 1f);
        InvokeRepeating("PowerUpSpawning", 20f, 30f);
        InvokeRepeating("CoronaVirusSpawning", 10f, 10f);

        Physics.IgnoreLayerCollision(11, 12, true);
    }

    private void Update()
    {   
        Randoms();
    }

    private void FixedUpdate() {
        CoronaManager.current.ChasingPlayer(player);
    }

    void CoinSpawning()
    {
        GameObject coin = Pooler.current.GetPooledCoins();
        int posAmount = Pooler.current.PooledCoinAmount;

        if(coin == null) return;

        for(int i = 0; i < posAmount ; i++)
        {
            coin.transform.position = randomPos[0];
            coin.SetActive(true);
        }
    }

    void PowerUpSpawning ()
    {
        List<GameObject> powerUps = Pooler.current.PowerUps;
        Vector3 loc = Pooler.current.PowerUpSpawnLocation;

        int powAmount = Pooler.current.PowerUpsAmount;
        int random = Random.Range(0, powAmount);

        if(powerUps[random] == null) return;
        
        powerUps[random].transform.position = loc;
        powerUps[random].SetActive(true);
    }

    void PrizeSpawning()
    {
        GameObject prize = Pooler.current.GetPrize();
        Vector3 loc = Pooler.current.PrizeBoxLocation;

        if(prize == null) return;

        prize.transform.position = loc;
        prize.SetActive(true);
    }

    void CoronaVirusSpawning ()
    {
        GameObject coronaVirus = Pooler.current.GetPooledCorona();
        int coronaAmount = Pooler.current.PooledCoronaAmount;

        if(coronaVirus == null) return;

        for (int i = 0 ; i < coronaAmount ; i++)
        {
            coronaVirus.transform.position = randomPos[2];
            coronaVirus.SetActive(true);
        }
    }

    private void Randoms(){
        Vector3 coinPos = Random.onUnitSphere * (objectPosOnPlanet);
        Vector3 powerUpPos = Random.onUnitSphere * (objectPosOnPlanet);
        Vector3 coronaPos = Random.onUnitSphere * (objectPosOnPlanet);

        randomPos[0] = coinPos;
        randomPos[1] = powerUpPos;
        randomPos[2] = coronaPos;
    }    
}
