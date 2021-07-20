using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    private GravityAttractor planet;
    private Tweener tweener;

    private void OnEnable() {
        planet = GameObject.FindObjectOfType<GravityAttractor>();
        Attract();
        tweener = this.transform.DOLocalRotate(new Vector3(0.0f, 180f, 0.0f), 1.25f, RotateMode.LocalAxisAdd).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear).SetRelative();
    }

    private void OnDisable() {
        Attract();
        tweener.Pause();
    }

    private void Attract()
    {
        if(!this.gameObject.activeInHierarchy) return;

        planet.AttractOtherObject(this.transform);
    }
}
