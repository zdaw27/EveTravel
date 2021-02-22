using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Text;

public class LevelUpTextEffect : BaseEffect
{
    [SerializeField]
    TMPro.TextMeshPro text;
    private bool isEffectEnd = false;

    public override void StartEffect(int damage = 0)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
        isEffectEnd = false;
        transform.DORotate(new Vector3(0, 0, 1440), 2f, RotateMode.FastBeyond360).SetEase(Ease.OutQuint);
        transform.DOMoveY(transform.position.y + 1f, 2f).SetEase(Ease.OutQuint);
        text.DOFade(0f, 1f).SetDelay(1f).OnComplete(Complete);
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
