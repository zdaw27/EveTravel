using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EveTravel
{
    [CreateAssetMenu(fileName = "UIObserver", menuName = "ScriptableObjects/UIObserver", order = 1)]
    public class UIObserver : ScriptableObject
    {
        private JoyStickDir _joyStickDir;

        public JoyStickDir JoyStickDir { get { return _joyStickDir; } set {
                _joyStickDir = value;
                OnJoyStick(value); } }

        public Action<JoyStickDir> OnJoyStick { get; set; }
        public Action OnAttakcButton { get; set; }
    }

    public enum JoyStickDir
    {
        Left,
        Right,
        Up,
        Down
    }

}