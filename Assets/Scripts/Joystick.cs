using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class Joystick : MonoBehaviour
    {
        [SerializeField] private UIObserver uiObserver = null;

        public void OnLeftButton()
        {
            uiObserver.JoyStickDir = JoyStickDir.Left;
        }

        public void OnRightButton()
        {
            uiObserver.JoyStickDir = JoyStickDir.Right;
        }

        public void OnUpButton()
        {
            uiObserver.JoyStickDir = JoyStickDir.Up;
        }

        public void OnDownButton()
        {
            uiObserver.JoyStickDir = JoyStickDir.Down;
        }
    }
}