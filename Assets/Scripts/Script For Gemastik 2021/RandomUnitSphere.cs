using UnityEngine;

public class RandomUnitSphere : MonoBehaviour
{
    void Update()
    {
        Vector3 start = Random.onUnitSphere;
        Debug.DrawLine(start - Vector3.forward * 0.01f, start + Vector3.forward * 0.01f, Color.green, 50.0f);
        Debug.DrawLine(start - Vector3.right * 0.01f, start + Vector3.right * 0.01f, Color.green, 50.0f);
        Debug.DrawLine(start - Vector3.up * 0.01f, start + Vector3.up * 0.01f, Color.green, 50.0f);
    }
}
