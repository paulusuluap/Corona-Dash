using UnityEngine;
using TMPro;

public class MathLerpTest : MonoBehaviour
{
    [Range(0f, 5f)] public float lerpTime = 4f;
    private float directionChange = 0f;
    private float directionSmooth = 0f;
    public TextMeshProUGUI lerpTest;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.N))
        {
            // if(directionChange >= .0f) directionChange -= 100f;
            directionChange -= 100f;

            // else directionChange += 100f;
        }

        if(Input.GetKey(KeyCode.M))
        {
            // if(directionChange <= .0f) directionChange += 100f;
            directionChange += 100f;

            // else directionChange -= 100f;
        }

        directionSmooth = Mathf.Lerp(directionSmooth, directionChange, lerpTime * Time.deltaTime);
        float clamp = Mathf.Clamp(directionSmooth, -100f, 100f);
        
        lerpTest.text = clamp.ToString("000");
    }
}
