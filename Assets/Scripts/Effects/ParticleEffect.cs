using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : BaseEffect
{
    [SerializeField] private ParticleSystem particle;

    public override void StartEffect()
    {
        particle.Play(true);
    }

    protected override bool CheckEffectEnd()
    {
        return particle.IsAlive(true);
    }
}
