using System.Collections;
using UnityEngine;

public class CameraBackgroudColor : MonoBehaviour
{
    private Color color1 = new Vector4(1f, 0.8511444f, 0.740566f);
    private Color color2 = new Vector4(0.22f, 0.01f, 0.36f);
    private float duration = 60f;
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
