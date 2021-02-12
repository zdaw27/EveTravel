using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : BaseEffect
{
    [SerializeField] private ParticleSystem particle;

    public override void StartEffect(int damage = 0)
    {
        particle.Play(true);
    }

    public override bool CheckEffectEnd()
    {
        return !particle.IsAlive(true);
    }
}
