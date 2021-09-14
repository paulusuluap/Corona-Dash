using System.Collections;
using UnityEngine;

public class PowerUpsController : MonoBehaviour
{
    public static PowerUpsController Instance;    
    private float radius = 10f;
    private float magnetSpeed = 20f;

    //Power Ups Durations
    private float magnetDuration;
    private float maskDuration;
    private float invincibleDuration;
    private float multiplierDuration;
    
    //Initial power ups duration
    private float initMagnetDur;
    private float initMaskDur;
    private float initInvincibleDur;
    private float initMultiplierDur;

    //Power ups activation
    protected bool isMagnetActive = false;
    protected bool isMaskActive = false;
    protected bool isInvincibleActive = false; 
    protected bool isMultiplierActive = false; 
    protected bool isFadingBack = false;     
    protected bool isFadingComplete = false;     

    public bool IsMagnetActive
    {
        get => isMagnetActive; 
        set => isMagnetActive = value; 
    }
    public bool IsMaskActive
    {
        get => isMaskActive; 
        set => isMaskActive = value; 
    }
    public bool IsInvincibleActive
    {
        get => isInvincibleActive;
        set => isInvincibleActive = value;
    }
    public bool IsMultiplierActive
    {
        get => isMultiplierActive;
        set => isMultiplierActive = value;
    }

    [SerializeField] Collider[] hitColliders = new Collider[10];

    private void Start() {
        Instance = this;

        magnetDuration = SaveManager.Instance.MagnetDuration;
        maskDuration = SaveManager.Instance.MaskDuration;
        invincibleDuration = SaveManager.Instance.InvincibleDuration;
        multiplierDuration = SaveManager.Instance.MultiplierDuration;

        initMagnetDur = magnetDuration;
        initMaskDur = maskDuration;
        initInvincibleDur = invincibleDuration;
        initMultiplierDur = multiplierDuration; 
    }

    //MAGNET
    protected void MagnetEffect(FirstPersonController player) 
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
    }
    public void Magnetize(FirstPersonController player)
    {        
        MagnetEffect(player);
        UIManager.current.MagnetIconCalled(initMagnetDur, magnetDuration -= Time.deltaTime);
    }

    //SUPER MASK    
    public void GiveMaskToPlayer()
    {        
        UIManager.current.MaskIconCalled(initMaskDur, maskDuration -= Time.deltaTime);
    }

    //INVINCIBLE

    IEnumerator InvincibleAbility()
    {
        isInvincibleActive = true;        
        Physics.IgnoreLayerCollision(8, 11, true);
        Physics.IgnoreLayerCollision(8, 12, true);

        yield return new WaitForSeconds(invincibleDuration);

        isFadingBack = true;
        Physics.IgnoreLayerCollision(8, 11, false);
        Physics.IgnoreLayerCollision(8, 12, false);

        yield return new WaitForSeconds(1f);  

        isFadingComplete = true;

        yield return new WaitForSeconds(.1f);
        
        isFadingBack = false;
        isFadingComplete = false;
        isInvincibleActive = false;
        invincibleDuration = SaveManager.Instance.InvincibleDuration; //reset duration
    }
    protected void InvincibleBegin(Renderer myRenderTransparent, Renderer myRenderOpaque, Color color)
    {        
        if(isInvincibleActive)
        {
            if(!isFadingBack)
            {
                color.a -= 0.5f; 
            
                Invincibility.instance.SetOpaqueRenderer(myRenderOpaque, false);
                Invincibility.instance.Fading(myRenderTransparent, color);
                Invincibility.instance.FadeT(color.a);            
            }
            else
            {
                color.a += 0.5f;             
                Invincibility.instance.Fading(myRenderTransparent, color);
                Invincibility.instance.FadeT(color.a);                         

                if(isFadingComplete)
                Invincibility.instance.SetOpaqueRenderer(myRenderOpaque, true);
            }            
        }                 
    }

    public void Invincible(Renderer myRenderTransparent, Renderer myRenderOpaque, Color color)
    {
        InvincibleBegin(myRenderTransparent, myRenderOpaque, color);
        UIManager.current.InvincibleIconCalled(initInvincibleDur, invincibleDuration -= Time.deltaTime);
    }

    //MULTIPLIER
    public void MultiplyScore()
    {        
        UIManager.current.MultiplierIconCalled(initMultiplierDur, multiplierDuration -= Time.deltaTime);
    }


    //Coroutine for Powerups Time to Deactivate

    protected IEnumerator MagnetAbility()
    {        
        isMagnetActive = true;

        yield return new WaitForSeconds(magnetDuration);
            
        isMagnetActive = false;
        magnetDuration = SaveManager.Instance.MagnetDuration;
    }
    protected IEnumerator SuperMaskAbility()
    {        
        isMaskActive = true;

        yield return new WaitForSeconds(maskDuration);

        isMaskActive = false;
        maskDuration = SaveManager.Instance.MaskDuration;
    }

    protected IEnumerator ScoreMultiplyAbility()
    {    
        isMultiplierActive = true; 

        UIManager.current.ScoreValue = 2;

        yield return new WaitForSeconds(multiplierDuration);

        UIManager.current.ScoreValue = 1;

        isMultiplierActive = false;
        multiplierDuration = SaveManager.Instance.MultiplierDuration;
    }

    public void ActivateMagnet()
    {
        StartCoroutine(MagnetAbility());
    }
    public void ActivateMask()
    {
        StartCoroutine(SuperMaskAbility());
    }
    public void ActivateInvincibility()
    {
        StartCoroutine(InvincibleAbility());
    }
    public void ActivateMultiplier()
    {
        StartCoroutine(ScoreMultiplyAbility());
    }

    
}
