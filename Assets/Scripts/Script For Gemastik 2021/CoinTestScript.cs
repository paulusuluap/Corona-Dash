using UnityEngine;
using DG.Tweening;

public class CoinTestScript : MonoBehaviour
{
    public GravityAttractor planet;
    private void OnEnable() {
        planet = GameObject.FindObjectOfType<GravityAttractor>();
        this.transform.DOLocalRotate(new Vector3(0.0f, 180f, 0.0f), .5f, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).SetRelative();
    }

    private void Update() {
        Rotate();
    }
    private void OnTriggerEnter(Collider collider) {
        if(collider.gameObject.CompareTag("Player"))
        {
            Destroy();
        }
    }

    private void Destroy () {
        this.gameObject.SetActive(false);
    }

    private void Rotate()
    {
        if(!this.gameObject.activeInHierarchy) return;

        planet.AttractOtherObject(this.transform);
    }

}
