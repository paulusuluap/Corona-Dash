using UnityEngine;

public class TestInvincible : MonoBehaviour
{
    public float defaultTransparency = 1f;
    public float fadeDuration = 1f;
    float currentTransparency;  
    float toFadeTo;
    float tempDist;
    bool isFadingUp;
    bool isFadingDown;
    Renderer myRender;
    Color myColor;
     
    void Start()
    {
        currentTransparency = defaultTransparency;
        
        myRender = this.gameObject.GetComponent<Renderer>();
        myColor = myRender.material.GetColor("_BaseColor");
        ApplyTransparency();
    }
     
    void ApplyTransparency(){
        myRender.material.SetColor("_BaseColor",new Color(myColor.r, myColor.g, myColor.b, currentTransparency));
    }
 
    public void SetT(float newT){
        currentTransparency = newT;
        ApplyTransparency();
    }
     
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

    private void Update() {
        
        Fading();

        if(Input.GetKeyDown(KeyCode.T))
        {
            if(myColor.a < defaultTransparency) 
            {
                myColor.a += defaultTransparency/2;
                FadeT(defaultTransparency);
            }
            else
            {
                myColor.a -= defaultTransparency/2;
                FadeT(defaultTransparency/2);
            }
        }
    }

    public void Fading()
    {
        if(isFadingUp){
            if(currentTransparency < toFadeTo){
                currentTransparency += (tempDist/fadeDuration) * Time.deltaTime;
                ApplyTransparency();
            }else{
                isFadingUp = false;
            }
        }
        else if(isFadingDown){
            if(currentTransparency > toFadeTo){
                currentTransparency -= (tempDist/fadeDuration) * Time.deltaTime;
                ApplyTransparency();
            }else{
                isFadingDown = false;
            }
        }
    }

}
