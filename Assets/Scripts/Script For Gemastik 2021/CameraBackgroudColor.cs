using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackgroudColor : MonoBehaviour
{
    private Color color1 = new Vector4(1f, 0.6039216f, 0.3333333f);
    private Color color2 = new Vector4(0.09019608f, 0.3568628f, 0.2f);
    private Color color3 = new Vector4(0.5843138f, 0.1215686f, 0.3098039f);
    private Color color4 = new Vector4(0.9176471f, 0.2745098f, 0.1843137f);
    private Color color5 = new Vector4(0f, 0.427451f, 0.6352941f);
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
        // float t = Mathf.PingPong(Time.time, duration) / duration;
        // m_camera.backgroundColor = Color.Lerp(color1, color2, t);

        BackgroundColorSetter();
    }

    private void BackgroundColorSetter()
    {
        for(int i = 0 ; i < swipe.instance.Pos.Length ; i++)
        {
            if(swipe.instance.ScrollPos < swipe.instance.Pos[i] + (swipe.instance.Distance / 4) && swipe.instance.ScrollPos > swipe.instance.Pos[i] - (swipe.instance.Distance / 4))
            {
                m_camera.backgroundColor = colorSetter[i];
                return;
            }
        }
    }

    private void AddColorsTolist()
    {
        colorSetter.Add(color1);
        colorSetter.Add(color2);
        colorSetter.Add(color3);
        colorSetter.Add(color4);
        colorSetter.Add(color5);
    }
}
