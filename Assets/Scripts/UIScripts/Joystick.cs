using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class Joystick : MonoBehaviour
    {
        [SerializeField]
        private JoystickObserver joystickObserver;

        public void OnLeftButton()
        {
            Debug.Log("clicked");
            joystickObserver.JoyStickDir = JoyStickDir.Left;
        }

        public void OnRightButton()
        {
            joystickObserver.JoyStickDir = JoyStickDir.Right;
        }

        public void OnUpButton()
        {
            joystickObserver.JoyStickDir = JoyStickDir.Up;
        }

        public void OnDownButton()
        {
            joystickObserver.JoyStickDir = JoyStickDir.Down;
        }
    }

    public enum JoyStickDir
    {
        Left,
        Right,
        Up,
        Down,
        None
    }
}