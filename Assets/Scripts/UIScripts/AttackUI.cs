using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class AttackUI : MonoBehaviour
    {
        [SerializeField]
        private UIObserver uiObserver;

        public void OnAttackButton()
        {
            uiObserver.OnAttakcButton();
        }
    }
}
