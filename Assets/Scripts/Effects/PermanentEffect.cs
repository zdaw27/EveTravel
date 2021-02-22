using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PermanentEffect : BaseEffect
{
    [SerializeField]
    private AnimationCurve curve;
    [SerializeField]
    private float effectSpeed = 1f;
    [SerializeField]
    private SpriteRenderer sprtieRenderer;

    private float effectTime = 0f;
    private Vector3 originalScale;
    private bool isEffectEnd = false;

    public override bool CheckEffectEnd()
    {
        return isEffectEnd;
    }

    public override void StartEffect(int damage = 0)
    {
        transform.DORewind();
        sprtieRenderer.DORewind();
        transform.localScale = originalScale;
        effectTime = 0f;
        isEffectEnd = false;
        transform.DORotate(new Vector3(0, 0, 360), 0.5f, RotateMode.FastBeyond360);
    }

    public override void EndEffect()
    {
        transform.DOScale(0f, 0.3f);
        sprtieRenderer.DOFade(0f, 0.3f).OnComplete(() => 
        {
            sprtieRenderer.DORewind();
            isEffectEnd = true;
        });
    }

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (!isEffectEnd)
        {
            effectTime += Time.deltaTime * effectSpeed;
            float evaluated = curve.Evaluate(effectTime);
            transform.localScale = originalScale * evaluated;
        }
    }
}
