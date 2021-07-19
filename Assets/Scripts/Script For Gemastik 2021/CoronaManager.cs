using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoronaManager : MonoBehaviour
{
    public static CoronaManager current;
    protected readonly Vector3 k_HalfExtentsBox = new Vector3 (25.0f, 25.0f, 25.0f); //Perhatikan lagi
    public float coronaSpeed = 5f;
    private float chaseDuration = 10f;
    private float resetChaseDuration;
    [SerializeField] Collider[] hitColliders = new Collider[30];
    private GravityAttractor planet;

    private void Awake() {
        current = this;
        resetChaseDuration = chaseDuration;
        planet = FindObjectOfType<GravityAttractor>();
    }

    protected void DetectPlayer(FirstPersonController player) 
    {
        foreach(GameObject corona in Pooler.current.PooledCoronas)
        {
            int nb = Physics.OverlapBoxNonAlloc(corona.transform.position, k_HalfExtentsBox, hitColliders, corona.transform.rotation);
            Corona virus = corona.GetComponent<Corona>(); 

            for(int i = 0 ; i < nb ; i++)
            {
                FirstPersonController playerDetected = hitColliders[i].GetComponent<FirstPersonController>();

                if(playerDetected != null) 
                {
                    chaseDuration -= Time.deltaTime;

                    virus.CoronaMoveToPlayer(player, coronaSpeed);
                    CoronaRotation(corona, player);
                }
                
                if(chaseDuration <= .0f) 
                {
                    corona.SetActive(false);
                    chaseDuration = resetChaseDuration;
                }
            }
        }
    }

    public void ChasingPlayer(FirstPersonController player)
    {
        DetectPlayer(player);
    }

    protected void CoronaRotation(GameObject corona, FirstPersonController player)
    {
        Vector3 targetDir = (corona.transform.position - player.transform.position).normalized;
        Quaternion targetRotation = Quaternion.FromToRotation(corona.transform.up, targetDir) * corona.transform.rotation;
        Quaternion newRotation = Quaternion.Slerp(corona.transform.rotation, targetRotation, .15f);

        transform.rotation = newRotation;
    }
}
