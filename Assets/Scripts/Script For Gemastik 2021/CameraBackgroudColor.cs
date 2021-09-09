using System.Collections.Generic;
using UnityEngine;

public class CameraBackgroudColor : MonoBehaviour
{   
    private Vector4 color_1 = new Color(0f, 0.427451f, 0.6352941f);
    private Vector4 color_2 = new Color(0.09019608f, 0.3568628f, 0.2f);
    private Vector4 color_3 = new Color(0.5843138f, 0.1215686f, 0.3098039f);
    private Vector4 color_4 = new Color(0.9176471f, 0.2745098f, 0.1843137f);
    private Vector4 color_5 = new Color(1f, 0.6039216f, 0.3333333f);

    private List<Color> colorSetter = new List<Color>(5);
    private float duration = 60f;
    private Camera m_camera;


    void Start()
    {
        m_camera = GetComponent<Camera>();
        m_camera.clearFlags = CameraClearFlags.SolidColor;

        AddColorsTolist();
    }

    void Update()
    {
        if(Input.touchCount == 0) return;
        BackgroundColorSetter();

        // float t = Mathf.PingPong(Time.time, duration) / duration;
        // m_camera.backgroundColor = Color.Lerp(color1, color2, t);
    }

    private void BackgroundColorSetter()
    {
        for(int i = 0 ; i < swipe.instance.Pos.Length ; i++)
        {
            if(swipe.instance.ScrollPos < swipe.instance.Pos[i] + (swipe.instance.Distance / 2) && swipe.instance.ScrollPos > swipe.instance.Pos[i] - (swipe.instance.Distance / 2))
            {
                m_camera.backgroundColor = colorSetter[i];

                //Check Unlocked Worlds wheter the Tap To Play or Price appear
                if(SaveManager.Instance.WorldsUnlocked[i])
                {
                    ShopSystem.Instance.play.gameObject.SetActive(true);
                    ShopSystem.Instance.buy.gameObject.SetActive(false);
                }
                else
                {
                    ShopSystem.Instance.play.gameObject.SetActive(false);
                    ShopSystem.Instance.buy.gameObject.SetActive(true);
                    ShopSystem.Instance.price.text = ShopSystem.Instance.worldsPrice[i].ToString() + " $";                    
                    ShopSystem.Instance.price.color = colorSetter[i];
                }

                return;
            }
        }
    }

    private void AddColorsTolist()
    {
        colorSetter.Add(color_1);
        colorSetter.Add(color_2);
        colorSetter.Add(color_3);
        colorSetter.Add(color_4);
        colorSetter.Add(color_5);
    }
}
