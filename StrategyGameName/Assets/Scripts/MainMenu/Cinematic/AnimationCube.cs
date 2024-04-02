using DG.Tweening;

using UnityEngine;

public class AnimationCube : MonoBehaviour
{
    private void Start()
    {
        var newTransform = transform.position + new Vector3(0, 4, 0);
        transform.DOMove(newTransform, 2).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}