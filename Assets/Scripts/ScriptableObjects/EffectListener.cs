using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "EffectListener", menuName = "ScriptableObjects/EffectListener", order = 1)]
public class EffectListener : ScriptableObject
{
    public Action<Vector3, EffectManager.EffectType, int> OnRaiseEffect { get; set; }

    public void RaiseEffect(Vector3 position, EffectManager.EffectType effectType, int damage = 0)
    {
        OnRaiseEffect(position, effectType, damage);
    }
}
