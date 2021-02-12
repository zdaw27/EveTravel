using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Text;

public class DamageNumberEffect : BaseEffect
{
    [SerializeField] TMPro.TextMeshPro text;
    [SerializeField] DOTweenAnimation[] tweenAnim;

    static StringBuilder stringBuilder = new StringBuilder();
    private bool isComplete = false;

    public override void StartEffect(int damage = 0)
    {
        stringBuilder.Clear();
        stringBuilder.Append(damage);
        text.text = stringBuilder.ToString();
        isComplete = false;
        transform.DOShakePosition(1f, 0.4f, 50, 80f);
        for (int i = 0; i < tweenAnim.Length; ++i)
        {
            tweenAnim[i].DORestart();
        }
    }

    public override bool CheckEffectEnd()
    {
        return isComplete;
    }

    public void OnComplete()
    {
        isComplete = true;
    }
}
