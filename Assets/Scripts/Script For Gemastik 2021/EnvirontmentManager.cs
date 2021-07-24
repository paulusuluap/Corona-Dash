using UnityEngine;

public class EnvirontmentManager : MonoBehaviour
{
    [ExecuteInEditMode]
    private GravityAttractor attractor;

    private void Start() {
        attractor = GameObject.FindWithTag("Planet").GetComponent<GravityAttractor>();
    }
    void Update()
    {
        attractor = GameObject.FindWithTag("Planet").GetComponent<GravityAttractor>();
        foreach (Transform obs in transform)
        {
            attractor.AttractOtherObject(obs.transform);
        }
    }
}
