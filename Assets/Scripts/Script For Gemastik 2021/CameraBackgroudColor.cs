using System.Collections;
using UnityEngine;

public class CameraBackgroudColor : MonoBehaviour
{
    private Color color1 = Color.red;
    private Color color2 = Color.blue;
    private float duration = 3f;
    private Camera m_camera;
    void Start()
    {
        m_camera = GetComponent<Camera>();
        m_camera.clearFlags = CameraClearFlags.SolidColor;
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PingPong(Time.time, duration) / duration;
        m_camera.backgroundColor = Color.Lerp(color1, color2, t);
    }
}
