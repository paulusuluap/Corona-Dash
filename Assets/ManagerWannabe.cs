using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerWannabe : MonoBehaviour
{
    //Script sebelum ke Testing Manager Script
    FirstPersonController player;
    private float offset = 8.5f;
    private float planetRadius;
    private float objectPosOnPlanet;
    GravityAttractor earthGravity;
    Vector3[] randomPos;


    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
        earthGravity = this.gameObject.GetComponent<GravityAttractor>();

        planetRadius = this.transform.localScale.x/2;
        objectPosOnPlanet = planetRadius + offset;

        randomPos = new Vector3[3];

        InvokeRepeating("CoronaViruses", 2f, 10f);
    }
    void Update()
    {
        RandomPositions();
    }

    private void FixedUpdate() {
        CoronaManager.current.ChasingPlayer(player);    
    }

    void CoronaViruses ()
    {
        GameObject coronaVirus = Blank.current.GetPooledCorona();
        int coronaAmount = Blank.current.pooledCoronaAmount;

        if(coronaVirus == null) return;

        for (int i = 0 ; i < coronaAmount ; i++)
        {
            coronaVirus.transform.position = randomPos[0];
            coronaVirus.SetActive(true);
        }
    }

    private void RandomPositions(){
        
        Vector3 coronaPos = Random.onUnitSphere;

        randomPos[0] = coronaPos;
    }    
}

