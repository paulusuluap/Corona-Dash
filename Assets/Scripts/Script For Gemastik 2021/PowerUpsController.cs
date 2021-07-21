using UnityEngine;
using System.Collections;

public class PowerUpsController : MonoBehaviour
{
    private float radius = 10f;
    private float magnetSpeed = 20f;
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

    IEnumerator Invincible(Renderer myRenderTransparent, Renderer myRenderOpaque, Color c)
    {
        c.a = 1f;
        
        Physics.IgnoreLayerCollision(8, 11, true);
        Physics.IgnoreLayerCollision(8, 12, true);

        c.a -= 0.5f;
        
        Invincibility.instance.SetOpaqueRenderer(myRenderOpaque, false);
        Invincibility.instance.Fading(myRenderTransparent, c);
        Invincibility.instance.FadeT(c.a);

        yield return new WaitForSeconds(invincibleDuration);
        
        Physics.IgnoreLayerCollision(8, 11, false);
        Physics.IgnoreLayerCollision(8, 12, false);

        Invincibility.instance.Fading(myRenderTransparent, c);

        c.a += 0.5f;
        Invincibility.instance.FadeT(c.a);

        yield return new WaitForSeconds(1.25f);

        Invincibility.instance.SetOpaqueRenderer(myRenderOpaque, true);

        isInvincible = false;
    }

    public void Magnetizing(FirstPersonController player)
    {
        Magnetize(player);
    }

    public void Invulnerable(Renderer myRenderTransparent, Renderer myRenderOpaque, Color c)
    {
        StartCoroutine(Invincible(myRenderTransparent, myRenderOpaque, c));
    }
}
