using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class EffectManager : MonoBehaviour
{
    [Serializable]
    public enum EffectType
    {
        PlayerHit = 0,
        EnemyHit,
        PlayerCriticalHit,
        EnemyCriticalHit,
    }

    [SerializeField] private EffectListener effectListener;
    [SerializeField] private SerializableEffectDictionary effectPrefabs;


    private void Awake()
    {
        effectListener.OnRaiseEffect += RaiseEffect;
    }

    public void Update()
    {
        
    }

    private void RaiseEffect(Vector3 raisePosition, EffectType type)
    {

    }
}
