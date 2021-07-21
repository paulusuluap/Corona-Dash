using UnityEngine;
using System.Collections;

public class PowerUpsController : MonoBehaviour
{
    [SerializeField] private float radius = 10f;
    public float magnetSpeed = 20f;
    private float magnetDuration = 10f;
    private float invincibleDuration = 10f;
    private Vector3 playerPosStoring;
    protected bool isMagnetized = false; 
    protected bool isInvincible = false; 
    public bool IsMagnetized {get { return isMagnetized;} set {isMagnetized = value;} }
    public bool IsInvincible {get { return isInvincible;} set {isInvincible = value;} }
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
        {
            magnetDuration = 10f;
            isMagnetized = false;
        }
    }

    IEnumerator Invincible(Renderer playerRender, Color c)
    {
        c.a = 1f;
        
        Physics.IgnoreLayerCollision(8, 11, true);
        Physics.IgnoreLayerCollision(8, 12, true);

        c.a -= 0.5f;
        Invincibility.instance.FadeT(c.a);
        Invincibility.instance.Fading(playerRender, c);

        yield return new WaitForSeconds(invincibleDuration);

        Invincibility.instance.Fading(playerRender, c);
        
        Physics.IgnoreLayerCollision(8, 11, false);
        Physics.IgnoreLayerCollision(8, 12, false);

        c.a += 0.5f;
        Invincibility.instance.FadeT(c.a);

        isInvincible = false;
    }

    public void Magnetizing(FirstPersonController player)
    {
        Magnetize(player);
    }

    public void Invulnerable(Renderer playerRender, Color c)
    {
        StartCoroutine(Invincible(playerRender, c));
    }
}
