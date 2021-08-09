using System.Collections;
using UnityEngine;

public class PowerUpsController : MonoBehaviour
{
    private float radius = 10f;
    private float magnetSpeed = 20f;
    private float magnetDuration = 10f;
    private float invincibleDuration = 10f;
    protected bool isMagnetized = false; 
    protected bool isInvincible = false; 
    public bool IsMagnetized {get { return isMagnetized;} set {isMagnetized = value;} }
    public bool IsInvincible {get { return isInvincible;} set {isInvincible = value;} }
    public float MagnetDuration {get { return magnetDuration; }}
    public float InvincibleDuration {get { return invincibleDuration; }}
    [SerializeField] Collider[] hitColliders = new Collider[10];

    protected void Magnetize(FirstPersonController player) 
    {
        int nb = Physics.OverlapSphereNonAlloc(player.transform.position, radius, hitColliders);
        
        for(int i = 0 ; i < nb ; i++)
        {
            Collectibles coinMesh = hitColliders[i].GetComponent<Collectibles>();

            if(coinMesh != null)
            {
                coinMesh.coinParent.transform.position = Vector3.MoveTowards(coinMesh.coinParent.transform.position, player.transform.position, magnetSpeed * Time.deltaTime);
            }
        }
        
        magnetDuration -= Time.deltaTime;
        // call the UI Magnet Icon here
        UIManager.current.MagnetIconCalled(magnetDuration);
        
        if(magnetDuration <= 0f)
        {
            magnetDuration = 10f;
            isMagnetized = false;
        }
    }
    public void Magnetizing(FirstPersonController player)
    {
        Magnetize(player);
    }

    IEnumerator Invincible(Renderer myRenderTransparent, Renderer myRenderOpaque, Color c)
    {
        c.a = 1f;
        
        Physics.IgnoreLayerCollision(8, 11, true);
        Physics.IgnoreLayerCollision(8, 12, true);

        c.a -= 0.5f; //alpha value set to 0.5
        
        Invincibility.instance.SetOpaqueRenderer(myRenderOpaque, false);
        Invincibility.instance.Fading(myRenderTransparent, c);
        Invincibility.instance.FadeT(c.a);
        
        yield return new WaitForSeconds(invincibleDuration);
        
        Physics.IgnoreLayerCollision(8, 11, false);
        Physics.IgnoreLayerCollision(8, 12, false);

        Invincibility.instance.Fading(myRenderTransparent, c);

        c.a += 0.5f; //alpha value set back to 1
        Invincibility.instance.FadeT(c.a);

        yield return new WaitForSeconds(1f);
        
        Invincibility.instance.SetOpaqueRenderer(myRenderOpaque, true);
        isInvincible = false;
        invincibleDuration = 10f; //reset duration
    }


    public void Invulnerable(Renderer myRenderTransparent, Renderer myRenderOpaque, Color c)
    {
        StartCoroutine(Invincible(myRenderTransparent, myRenderOpaque, c));
        UIManager.current.InvincibleIconCalled(invincibleDuration -= Time.deltaTime);
    }
}
