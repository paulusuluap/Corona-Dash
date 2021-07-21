using UnityEngine;

public class Invincibility : MonoBehaviour
{
    public static Invincibility instance;
    public float defaultTransparency = 1f;
    public float fadeDuration = 1f;
    float currentTransparency;  
    float toFadeTo;
    float tempDist;
    bool isFadingUp;
    bool isFadingDown;
    // Renderer myRender;
     
    void Awake()
    {
        instance = this;
        currentTransparency = defaultTransparency;
    }
     
    void ApplyTransparency(Renderer myRenderTransparent, Color c)
    {
        
        myRenderTransparent.material.SetColor("_BaseColor",new Color(c.r, c.g, c.b, currentTransparency));
    }
 
    // public void SetT(float newT){
    //     currentTransparency = newT;
    //     ApplyTransparency();
    // }
     
    public void FadeT(float newT){
        toFadeTo = newT;
        if(currentTransparency < toFadeTo){
            tempDist = toFadeTo - currentTransparency;
            isFadingUp = true;
        }else{
            tempDist = currentTransparency - toFadeTo;
            isFadingDown = true;
        }
    }

    public void Fading(Renderer myRenderTransparent, Color c)
    {
        if(isFadingUp){
            if(currentTransparency < toFadeTo){
                currentTransparency += (tempDist/fadeDuration) * Time.deltaTime;
                ApplyTransparency(myRenderTransparent, c);
                //Opaque enabled
            }else{
                isFadingUp = false;
            }
        }
        else if(isFadingDown){
            if(currentTransparency > toFadeTo){
                currentTransparency -= (tempDist/fadeDuration) * Time.deltaTime;
                ApplyTransparency(myRenderTransparent, c);
            }else{
                isFadingDown = false;
            }
        }
    }

    public void SetOpaqueRenderer(Renderer myRenderOpaque, bool whatMyRenderOpaque)
    {
        if(!whatMyRenderOpaque) myRenderOpaque.GetComponent<Renderer>().enabled = whatMyRenderOpaque;
        else myRenderOpaque.GetComponent<Renderer>().enabled = whatMyRenderOpaque;
    }
}
