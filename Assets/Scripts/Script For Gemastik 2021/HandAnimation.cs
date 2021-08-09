using DG.Tweening;
using UnityEngine;

public class HandAnimation : MonoBehaviour
{
    private void OnEnable() {
        this.transform.DOScale(new Vector3(0.9f, 0.9f, 1f), 1f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .SetUpdate(true);
    }
    private void OnDisable() {
        this.transform.DOKill();
    }
}
