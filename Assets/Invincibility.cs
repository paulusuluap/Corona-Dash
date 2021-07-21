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
    Renderer myRender;
    Color myColor;
     
    void Awake()
    {
        instance = this;
        currentTransparency = defaultTransparency;
        
        // myRender = this.gameObject.GetComponent<Renderer>();
        // myColor = myRender.material.GetColor("_BaseColor");

        // // ApplyTransparency();
    }
     
    void ApplyTransparency(Renderer myRender, Color c){
        myRender.material.SetColor("_BaseColor",new Color(c.r, c.g, c.b, currentTransparency));
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

    public void Fading(Renderer myRender, Color c)
    {
        if(isFadingUp){
            if(currentTransparency < toFadeTo){
                currentTransparency += (tempDist/fadeDuration) * Time.deltaTime;
                ApplyTransparency(myRender, c);
            }else{
                isFadingUp = false;
            }
        }
        else if(isFadingDown){
            if(currentTransparency > toFadeTo){
                currentTransparency -= (tempDist/fadeDuration) * Time.deltaTime;
                ApplyTransparency(myRender, c);
            }else{
                isFadingDown = false;
            }
        }
    }
}
