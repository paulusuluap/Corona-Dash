using UnityEngine;

public class CoronaManager : MonoBehaviour
{
    public static CoronaManager current;
    protected readonly Vector3 k_HalfExtentsBox = new Vector3 (15.0f, 15.0f, 15.0f); //Perhatikan lagi
    private float coronaSpeed = 7.5f;
    private float chaseDuration = 15f;
    private float coronaSpawnTime = 10f;
    public float CoronaSpeed {get { return coronaSpeed;} set {value = coronaSpeed;}}
    public float CoronaSpawnTime {get { return coronaSpawnTime;} set {value = coronaSpawnTime;}}
    private float resetChaseDuration;
    [SerializeField] Collider[] hitColliders = new Collider[30];
    private GravityAttractor planet;
    private FirstPersonController player;

    private void Awake() {
        current = this;
        resetChaseDuration = chaseDuration;
        planet = FindObjectOfType<GravityAttractor>();
        player = GameObject.FindWithTag("Player").GetComponent<FirstPersonController>();
    }

    private void FixedUpdate() {
        ChasingPlayer(player);
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

    // protected void CoronaRotation(GameObject corona, FirstPersonController player)
    // {
    //     Vector3 targetDir = (player.transform.position - corona.transform.position).normalized;
    //     Quaternion targetRotation = Quaternion.LookRotation(new Vector3(targetDir.x, 0f, targetDir.z));
    //     Quaternion newRotation = Quaternion.Slerp(corona.transform.rotation, targetRotation, Time.deltaTime * 5f);

    //     transform.rotation = newRotation;
    // }
}
