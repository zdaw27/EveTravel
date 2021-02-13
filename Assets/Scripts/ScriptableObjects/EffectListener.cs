using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "EffectListener", menuName = "ScriptableObjects/EffectListener", order = 1)]
public class EffectListener : ScriptableObject
{
    public Func<Vector3, EffectManager.EffectType, int, BaseEffect> OnRaiseEffect { get; set; }

    public BaseEffect RaiseEffect(Vector3 position, EffectManager.EffectType effectType, int damage = 0)
    {
        return OnRaiseEffect(position, effectType, damage);
    }
}
