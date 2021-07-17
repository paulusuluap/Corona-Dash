using UnityEngine;

public class GravityAttractor : MonoBehaviour
{       
    public float gravity = -10f;
    private Rigidbody rb;
    private void Awake() {
        rb = GameObject.FindWithTag("Player").GetComponent<Rigidbody>();
    }
    public void AttractPlayer(Transform body)
    {
        Vector3 bodyUp = body.up;  
        Vector3 targetDir = (body.position - transform.position).normalized;

        body.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;
        rb.AddForce(targetDir * gravity);
    }

    public void AttractOtherObject(Transform obj)
    {
        Vector3 objUp = obj.up;
        Vector3 targetDir = (obj.position - transform.position).normalized;

        obj.rotation = Quaternion.FromToRotation(objUp, targetDir) * obj.rotation;

        if(!obj.gameObject.activeInHierarchy)
            return;
    }
}
