using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EveTravel
{
    public class EffectTest : MonoBehaviour
    {
        [SerializeField]
        private EffectRaiser effectRaiser;

        private float time;
        private int effectIndex = 0;
        private int yOffset;
        // Start is called before the first frame update

        // Update is called once per frame
        void Update()
        {
            time += Time.deltaTime;
            if (time >= 0.2f)
            {
                
                time = 0;
                foreach (EffectManager.EffectType type in Enum.GetValues(typeof(EffectManager.EffectType)))
                {
                    effectRaiser.RaiseEffect(effectIndex * Vector3.right + yOffset * Vector3.down, type);
                    effectIndex++;
                }
                effectIndex = 0;
                yOffset++;

            }
        }
    }
}