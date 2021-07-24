using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyCycle : MonoBehaviour
{
    public float secondsPerMinute = 0.625f;
    public float startTime= 12.0f; 
    private bool showGUI = false;
    public float latitudeAngle = 45.0f;
    public Transform sunTilt;
    private Transform sunOrbit;
    private float day;
    private float min;
    [SerializeField] private float smoothMin;
    private float texOffset;
    private Material skyMat;

    void Start()
    {
        skyMat = GetComponent<Renderer>().sharedMaterial;
        sunOrbit = sunTilt.GetChild(0);

        float sunTiltSetterX = sunTilt.eulerAngles.x;
        sunTiltSetterX = Mathf.Clamp(latitudeAngle, 0f, 90f); //set the sun tilt

        if(secondsPerMinute == 0f){
            Debug.LogError("Error! Can't have a time of zero, changed to 0.01 instead.");
            secondsPerMinute = 0.01f;
	    }
    }

    void Update()
    {
        UpdatingSky();
    }

    void UpdatingSky()
    {
        smoothMin = (Time.time/secondsPerMinute) + (startTime * 60);
        day = Mathf.Floor(smoothMin / 1440f) + 1;

        smoothMin = smoothMin - (Mathf.Floor(smoothMin / 1440f) * 1440f); //clamp smoothMin between 0-1440
        min = Mathf.Round(smoothMin);
        
        float sunOrbitSmooth = sunOrbit.localEulerAngles.y;
        sunOrbitSmooth = smoothMin/4f;
        texOffset = Mathf.Cos((((smoothMin) / 1440f) * 2f) * Mathf.PI) * 0.25f + 0.25f;
        skyMat.mainTextureOffset = new Vector3(Mathf.Round((texOffset - (Mathf.Floor(texOffset / 360f) * 360f)) * 1000f)/1000f, 0f, 0f);
    }

    private string digitalDisplay(string num)
    {
        if(num.Length == 2) return num;
        else return "0" + num;
    }

    private void OnGUI() 
    {
        if(showGUI)
        {
		    GUI.Label(new Rect(10,0,100,20),"Day " + day.ToString());
		    GUI.Label(new Rect(10,20,100,40),digitalDisplay(Mathf.Floor(min/60).ToString()) + ":" + digitalDisplay((min-Mathf.Floor(min/60)*60).ToString()));
	    }
    }
}
