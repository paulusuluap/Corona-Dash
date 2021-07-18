using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestingManager : MonoBehaviour
{
    [SerializeField]
    private float offset = 8.5f;
    private float planetRadius;
    private float coinPosition;
    GravityAttractor earthGravity;
    Vector3[] randomPos;

    void Start()
    {
        planetRadius = this.transform.localScale.x/2;
        coinPosition = planetRadius + offset;
        randomPos = new Vector3[3];
        earthGravity = this.gameObject.GetComponent<GravityAttractor>();

        DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(2000, 100);

        InvokeRepeating("Coins", 2f, 1f);
        InvokeRepeating("PowerUps", 20f, 30f);
    }

    void Coins()
    {
        GameObject coin = Pooler.current.GetPooledCoins();
        int posAmount = Pooler.current.pooledCoinAmount;

        if(coin == null) return;

        for(int i = 0; i < posAmount ; i++)
        {
            coin.transform.position = randomPos[0];
            coin.SetActive(true);
            earthGravity.AttractOtherObject(coin.transform);
            coin.transform.DOLocalRotate(new Vector3(0.0f, 180f, 0.0f), .5f, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).SetRelative();
        }
    }

    void PowerUps ()
    {
        GameObject powerUp = Pooler.current.GetPooledPowerUps();
        int powerUpsAmount = Pooler.current.powerUpsAmount;

        if(powerUp == null) return;

        for (int i = 0 ; i < powerUpsAmount ; i++)
        {
            //Belum dibuat randomization untuk dapet power up apa
            powerUp.transform.position = randomPos[1];
            powerUp.SetActive(true);
            earthGravity.AttractOtherObject(powerUp.transform);
        }
    }

    private void RandomCoinPosition(){
        Vector3 pos1 = Random.onUnitSphere * (coinPosition);
        Vector3 powerUpPos = Random.onUnitSphere * (coinPosition);

        randomPos[0] = pos1;
        randomPos[1] = powerUpPos;
    }
    void Update()
    {   
        RandomCoinPosition();
    }
}
