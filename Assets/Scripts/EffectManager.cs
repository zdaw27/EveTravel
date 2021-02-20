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
        ClickEffect,
        DamageEffect,
        PermanentEffect,
        CoinEffect,
        LootingEffect,
        HealingEffect
    }

    [SerializeField] private EffectListener effectListener;
    [SerializeField] private SerializableEffectDictionary effectPrefabs;

    private Dictionary<EffectType, ObjectPool<BaseEffect>> pools = new Dictionary<EffectType, ObjectPool<BaseEffect>>();
    private Dictionary<EffectType, List<BaseEffect>> activedEffects = new Dictionary<EffectType, List<BaseEffect>>();

    private void Awake()
    {
        effectListener.OnRaiseEffect += RaiseEffect;

        foreach(KeyValuePair<EffectType, BaseEffect> element in effectPrefabs)
        {
            ObjectPool<BaseEffect> objPool = new ObjectPool<BaseEffect>(10, element.Value);
            pools.Add(element.Key, objPool);
            activedEffects.Add(element.Key, new List<BaseEffect>());
        }
    }

    public void Update()
    {
        foreach(KeyValuePair<EffectType, List<BaseEffect>> element in activedEffects)
        {
            for(int i = 0; i < element.Value.Count; ++i)
            {
                if(element.Value[i].CheckEffectEnd())
                {
                    pools[element.Key].RetrieveObject(element.Value[i]);
                    element.Value.RemoveAt(i);
                    --i;
                }
            }
        }
    }

    private BaseEffect RaiseEffect(Vector3 raisePosition, EffectType type, int damage = 0)
    {
        BaseEffect effect = pools[type].GetObject();
        effect.transform.position = new Vector3(raisePosition.x, raisePosition.y, -10f);
        effect.StartEffect(damage);
        activedEffects[type].Add(effect);

        if (effect is PermanentEffect)
            return effect;
        else
            return null;
    }
}
