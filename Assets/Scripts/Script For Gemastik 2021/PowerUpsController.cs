using System.Collections;
using UnityEngine;

public class PowerUpsController : MonoBehaviour
{
    [SerializeField] private float radius = 10f;
    public float magnetSpeed = 22.5f;
    private float magnetDuration = 10f;
    private Vector3 playerPosStoring;
    protected bool isMagnetized = false; 
    public bool Magnetized {get { return isMagnetized;} set {isMagnetized = value;} }
    public float MagnetDuration {get { return magnetDuration; } set {magnetDuration = value;} }
    [SerializeField] Collider[] hitColliders = new Collider[10];

    protected void Magnetize(FirstPersonController player) 
    {
        int nb = Physics.OverlapSphereNonAlloc(player.transform.position, radius, hitColliders);

        playerPosStoring = player.transform.position;
        
        for(int i = 0 ; i < nb ; i++)
        {
            Collectibles coinMesh = hitColliders[i].GetComponent<Collectibles>();

            if(coinMesh != null)
            {
                coinMesh.coinParent.transform.position = Vector3.MoveTowards(coinMesh.coinParent.transform.position, player.transform.position, magnetSpeed * Time.deltaTime);
            }
        }
        
        magnetDuration -= Time.deltaTime;
        
        if(magnetDuration <= 0f)
            isMagnetized = false;

        // if(!isMagnetized)
        //     return;
        
        // yield return new WaitForSecondsRealtime(magnetDuration);

        // isMagnetized = false;
    }

    public void Magnetizing(FirstPersonController player)
    {
        // StartCoroutine(Magnetize(player));
        Magnetize(player);
    }
}
