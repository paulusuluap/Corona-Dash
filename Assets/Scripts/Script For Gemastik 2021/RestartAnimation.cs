using DG.Tweening;
using UnityEngine;

public class RestartAnimation : MonoBehaviour
{
    private void OnEnable() {
        this.transform.DOScale(new Vector3(0.925f, 0.925f, 1f), 1.5f)
			.SetEase(Ease.InOutSine)
			.SetLoops(-1, LoopType.Yoyo);
    }
}
