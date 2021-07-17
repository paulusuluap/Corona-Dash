using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject target;
    private float speed = 5f;

    float minFov = 35f;
    float maxFov = 100f;
    float sensitivity = 17f;

    void Update()
    {
        if(Input.GetMouseButton(1))
        {
            transform.RotateAround(target.transform.position, transform.up, Input.GetAxis("Mouse X") * speed);
            transform.RotateAround(target.transform.position, transform.right, Input.GetAxis("Mouse Y") * -speed);
        }        

        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * -sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }
}
