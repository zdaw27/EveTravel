using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class UISmoothInOut : MonoBehaviour
{
    [SerializeField]
    private RectTransform target;
    [SerializeField]
    private UnityEvent OnEndOut;
    private const int height = 1080;
    private Vector3 originalPosition;

    private void Awake()
    {
        originalPosition = target.transform.position;
    }

    public void InSmooth()
    {
        target.position = new Vector3(originalPosition.x, originalPosition.y + height, originalPosition.z);
        target.DOMoveY(originalPosition.y, 0.5f).SetEase(Ease.OutBounce);
    }

    public void OutSmooth()
    {
        target.position = originalPosition;
        target.DOMoveY(originalPosition.y + height, 0.5f).SetEase(Ease.OutBounce).OnComplete(() => { OnEndOut.Invoke(); });
    }
}
