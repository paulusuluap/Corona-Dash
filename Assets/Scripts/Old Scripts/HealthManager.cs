using System.Collections;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public GameObject[] health;
    [SerializeField] private Vector3[] pos;
    private float planetScaleX;
    private Vector3 pos1, pos2, pos3;
    private GameObject[] HealthObjs;
    private Transform planetTransform;
    void Start()
    {
        StartCoroutine(HealthSpawn()); 
        pos = new Vector3[3]; 
        planetTransform = GameObject.FindWithTag("Planet").GetComponent<Transform>();
    }

    private void Update() {
        planetScaleX = planetTransform.localScale.x;   

        HealthPosition();
        HealthObjs = GameObject.FindGameObjectsWithTag("Health");
    }

    void HealthPosition()
    {
        Vector3 pos1 = Random.onUnitSphere * (planetScaleX / 2);
        Vector3 pos2 = Random.onUnitSphere * (planetScaleX / 2);
        Vector3 pos3 = Random.onUnitSphere * (planetScaleX / 2);

        pos[0] = pos1;
        pos[1] = pos2;
        pos[2] = pos3;
    }
    private IEnumerator HealthSpawn()
    {                               
        for(int i = 0 ; i < 3 ; i++)
        {
            Instantiate(health[i], pos[i], health[i].transform.rotation);
        }        

        yield return new WaitForSeconds(5f);

        foreach(var h in HealthObjs)
            Destroy(h);
        
        StartCoroutine(HealthSpawn());
    }
}