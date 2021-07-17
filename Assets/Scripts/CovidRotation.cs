using UnityEngine;

public class CovidRotation : MonoBehaviour
{
    public float speed;
    
    void Update()
    {
        transform.Rotate(new Vector3(1 , 0 , 0) * Time.deltaTime * speed, Space.World);       
    }
}
