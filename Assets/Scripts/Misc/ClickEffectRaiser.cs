using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickEffectRaiser : MonoBehaviour
{
    [SerializeField]
    private EffectRaiser effectRaiser;
    [SerializeField]
    private EffectManager.EffectType type = EffectManager.EffectType.ClickEffect;

    private BaseEffect effect;

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (effect != null)
                effect.EndEffect();

            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                effect = effectRaiser.RaiseEffect(worldPosition, type, 54);
            }
        }
    }
}
