using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    /// <summary>
    /// Scale 값을 이용해 현재 체력을 표현. (hPBarFront의 scale)
    /// </summary>
    public class HPBar : MonoBehaviour
    {
        [SerializeField] private Transform bar;
        public Character character { get; set; }

        private void Update()
        {
            bar.localScale = new Vector3(1f * ((float)character.Stat.hp / (float)character.Stat.maxHp), character.transform.localScale.y, character.transform.localScale.z);
        }
    }
}