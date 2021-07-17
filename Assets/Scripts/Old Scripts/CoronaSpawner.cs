using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CoronaSpawner : MonoBehaviour
{
    [SerializeField] private GameObject corona;
    [SerializeField] private List<GameObject> covidsPool;
    private Vector3 storedPos;
    [SerializeField] private GameObject _covidContainer;
    
    private void Update() {
        Vector3 pos = Random.onUnitSphere * 5;
        storedPos = pos;  
    }
    private IEnumerator CovSpawner()
    {
        GameObject cov = RequestCovidToFall();
        cov.transform.position = storedPos;
    
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(CovSpawner());
    }

    private void Start() {
        covidsPool = GenerateVirus(10);
        StartCoroutine(CovSpawner());
    }

    //Add covids to List and deactivate all
    List<GameObject> GenerateVirus(int amountOfCovids)
    {
        for(int i = 0; i < amountOfCovids; i++)
        {
            GameObject cov = Instantiate(corona);
            cov.transform.parent = _covidContainer.transform;
            cov.SetActive(false);

            covidsPool.Add(cov);                
        }
        return covidsPool;
    }

    //activating deactivated virus within array
    public GameObject RequestCovidToFall()
    {
        foreach(var cov in covidsPool)
        {
            if(cov.activeInHierarchy == false)
            {
                cov.SetActive(true);
                return cov;
            }
        }
        return null;
    }
}
