using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseEffect : MonoBehaviour
{
    abstract public void StartEffect(int damage = 0);
    abstract public bool CheckEffectEnd();
}
