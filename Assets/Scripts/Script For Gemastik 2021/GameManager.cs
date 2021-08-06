using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float offset = 8.5f;
    private float planetRadius;
    private float objectPosOnPlanet;
    private float randomPick;
    GravityAttractor planet;
    FirstPersonController player;
    Vector3[] randomPos;
    Vector3 cloudPosInit = new Vector3 (-13f, 119f, 234f); 
    Vector3 cloudPosEnd = new Vector3 (-13f, 119f, -71f);
    [Range(0f, 1f)] public float cloudLerp = 1f;
    GameObject clouds;

    void Start()
    {
        DOTween.Init(true, true, LogBehaviour.Verbose).SetCapacity(2000, 100);

        player = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAttractor>();
        clouds = GameObject.FindGameObjectWithTag("Clouds");
        
        planetRadius = this.transform.localScale.x/2;
        objectPosOnPlanet = planetRadius + offset;
        randomPos = new Vector3[4];

        float prizeInitalTime = Random.Range(30f, 60f);

        InvokeRepeating("PrizeSpawning", prizeInitalTime, 300f);
        InvokeRepeating("CoinSpawning", 2f, 1f);
        InvokeRepeating("PowerUpSpawning", 20f, 45f);
        InvokeRepeating("CoronaVirusSpawning", 10f, CoronaManager.current.CoronaSpawnTime);

        Physics.IgnoreLayerCollision(9, 11, true);
        Physics.IgnoreLayerCollision(11, 12, true);
    }

    private void Update()
    {   
        Randoms();
        CloudMove();
        StageSetter();
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

        ParticleManager.instance.IdleParticles("PowActive");
        powerUps[random].transform.position = loc;
        powerUps[random].SetActive(true);
    }

    void PrizeSpawning()
    {
        GameObject prize = Pooler.current.GetPrize();
        Vector3 loc = Pooler.current.PrizeBoxLocation;

        if(prize == null) return;
        ParticleManager.instance.IdleParticles("PrizeActive");
        prize.transform.position = loc;
        prize.SetActive(true);
    }

    void CoronaVirusSpawning ()
    {
        GameObject coronaVirus = Pooler.current.GetPooledCorona();
        Transform[] PosColletion = Pooler.current.coronaPositions;
        int coronaAmount = Pooler.current.PooledCoronaAmount;
        int random = Random.Range(0, PosColletion.Length);

        if(coronaVirus == null) return;

        for (int i = 0 ; i < coronaAmount ; i++)
        {
            coronaVirus.transform.position = PosColletion[random].position;
            coronaVirus.SetActive(true);
        }
    }

    private void Randoms(){
        Vector3 coinPos = Random.onUnitSphere * (objectPosOnPlanet);
        randomPos[0] = coinPos;
    }

    private void CloudMove()
    {
        if(!player.enabled) return; 

        float cloudSpeed = 25f;
        Vector3 cloudLerp = Vector3.Lerp(clouds.transform.position, cloudPosEnd, 10f);

        clouds.transform.position = Vector3.MoveTowards(clouds.transform.position, cloudLerp, cloudSpeed * Time.deltaTime);

        if(clouds.transform.position == cloudPosEnd)
        clouds.transform.position = cloudPosInit;
    }

    private void StageSetter()
    {
        if(UIManager.current.Score < 50) return;

        if(UIManager.current.Score >= 50 && UIManager.current.Score < 120)
        {
            player.WalkSpeed = 12.5f;
            player.TurnSpeed = 225f;
            CoronaManager.current.CoronaSpawnTime = 7.5f;
            CinemachineShake.Instance.ShakeCamera(5f, 0.25f);
            return;
        }

        if(UIManager.current.Score >= 120 && UIManager.current.Score < 205)
        {
            player.WalkSpeed = 15f;
            player.TurnSpeed = 270f;
            CoronaManager.current.CoronaSpawnTime = 5f;
            CoronaManager.current.CoronaSpeed = 8f;
            Pooler.current.stage1 = true;
            CinemachineShake.Instance.ShakeCamera(5f, 0.25f);
            return;
        }
        if(UIManager.current.Score >= 205 && UIManager.current.Score < 290)
        {
            player.WalkSpeed = 17.5f;
            player.TurnSpeed = 315f;
            CoronaManager.current.CoronaSpeed = 8.5f;
            CinemachineShake.Instance.ShakeCamera(5f, 0.25f);
            return;
        }
        if(UIManager.current.Score >= 290 && UIManager.current.Score < 375)
        {
            player.WalkSpeed = 20f;
            player.TurnSpeed = 360f;
            CoronaManager.current.CoronaSpawnTime = 3f;
            CoronaManager.current.CoronaSpeed = 9f;
            CinemachineShake.Instance.ShakeCamera(5f, 0.25f);
            Pooler.current.stage2 = true;
            return;
        }
    }
}
