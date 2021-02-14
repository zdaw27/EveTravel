using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffectRaiser : MonoBehaviour
{
    [SerializeField] private EffectListener effectListener;
    [SerializeField] private EffectManager.EffectType type = EffectManager.EffectType.ClickEffect;

    private BaseEffect effect;

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (effect != null)
                effect.EndEffect();
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            effect = effectListener.RaiseEffect(worldPosition, type, 54);

        }
    }
}
