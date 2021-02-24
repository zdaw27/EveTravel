using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class AttackUI : MonoBehaviour
    {
        [SerializeField]
        private GameEvent AttackButtonEvent;

        public void OnAttackButton()
        {
            AttackButtonEvent.Raise();
        }
    }
}
