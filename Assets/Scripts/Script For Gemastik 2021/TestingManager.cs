using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestingManager : MonoBehaviour
{
    [SerializeField]
    private float offset = 8.5f;
    private float planetRadius;
    private float objectPosOnPlanet;
    GravityAttractor planet;
    FirstPersonController player;
    Vector3[] randomPos;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();

        planetRadius = this.transform.localScale.x/2;
        objectPosOnPlanet = planetRadius + offset;
        randomPos = new Vector3[3];

        DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(2000, 100);

        InvokeRepeating("CoinSpawning", 2f, 1f);
        InvokeRepeating("PowerUpSpawning", 20f, 30f);
        InvokeRepeating("CoronaVirusSpawning", 5f, 10f);
    }


    //Ngetes aja, performa pasti ancur
    void SetAttractPooledItems(){
        //Attract anak

        foreach (Transform eachChild in transform) 
        {
            if (eachChild.gameObject.activeInHierarchy)
                planet.AttractOtherObject(eachChild.transform);    
        }  

    }

    private void Update()
    {   
        RandomPosition();
        SetAttractPooledItems();
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
            coin.transform.SetParent(this.transform);
            coin.SetActive(true);
            coin.transform.DOLocalRotate(new Vector3(0.0f, 180f, 0.0f), .5f, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).SetRelative();
        }
    }

    void PowerUpSpawning ()
    {
        GameObject powerUp = Pooler.current.GetPooledPowerUps();
        int powerUpsAmount = Pooler.current.PowerUpsAmount;

        if(powerUp == null) return;

        for (int i = 0 ; i < powerUpsAmount ; i++)
        {
            //Belum dibuat randomization untuk dapet power up apa
            powerUp.transform.position = randomPos[1];
            powerUp.transform.SetParent(this.transform);
            powerUp.SetActive(true);
        }
    }
    void CoronaVirusSpawning ()
    {
        GameObject coronaVirus = Pooler.current.GetPooledCorona();
        int coronaAmount = Pooler.current.pooledCoronaAmount;

        if(coronaVirus == null) return;

        for (int i = 0 ; i < coronaAmount ; i++)
        {
            coronaVirus.transform.position = randomPos[2];
            coronaVirus.transform.SetParent(this.transform);
            coronaVirus.SetActive(true);
        }
    }

    private void RandomPosition(){
        Vector3 coinPos = Random.onUnitSphere * (objectPosOnPlanet);
        Vector3 powerUpPos = Random.onUnitSphere * (objectPosOnPlanet);
        Vector3 coronaPos = Random.onUnitSphere * (objectPosOnPlanet);

        randomPos[0] = coinPos;
        randomPos[1] = powerUpPos;
        randomPos[2] = coronaPos;
    }    
}
