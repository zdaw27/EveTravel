using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEffectRaiser : MonoBehaviour
{
    [SerializeField] private EffectListener effectListener;
    [SerializeField] private EffectManager.EffectType type = EffectManager.EffectType.ClickEffect;

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            effectListener.RaiseEffect(worldPosition, type, 54);
        }
    }
}
