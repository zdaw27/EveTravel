using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LootingEffect : BaseEffect
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool isEffectEnd = false;

    public override void StartEffect(int damage = 0)
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        isEffectEnd = false;
        transform.DORotate(new Vector3(0, 0, 720), 2f, RotateMode.FastBeyond360).SetEase(Ease.OutQuint);
        transform.DOMoveY(transform.position.y + 1f, 2f).SetEase(Ease.OutQuint);
        spriteRenderer.DOFade(0f, 1f).SetDelay(1f).OnComplete(Complete);
    }

    private void Complete()
    {
        isEffectEnd = true;
    }

    public override bool CheckEffectEnd()
    {
        return isEffectEnd;
    }

    
}
