using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EveTravel
{
    [CreateAssetMenu(fileName = "UIObserver", menuName = "ScriptableObjects/UIObserver", order = 1)]
    public class JoystickObserver : ScriptableObject
    {
        private JoyStickDir joyStickDir;

        public JoyStickDir JoyStickDir { get { return joyStickDir; }
            set {
                joyStickDir = value;
                 }
        }
    }

    

}