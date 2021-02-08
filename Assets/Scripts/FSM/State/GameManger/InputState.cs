using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class InputState : IState<GameManager>
    {
        NPC player;
        bool isJoystickPushed = false;
        Vector3 direction;

        public InputState(NPC player, UIObserver uiObserver)
        {
            uiObserver.OnJoyStick += OnJoystickDir;
            this.player = player;
        }

        private void OnJoystickDir(JoyStickDir dir)
        {

            switch (dir)
            {
                case JoyStickDir.Left:
                    direction = Vector3.left;
                    break;
                case JoyStickDir.Right:
                    direction = Vector3.right;
                    break;
                case JoyStickDir.Up:
                    direction = Vector3.up;
                    break;
                case JoyStickDir.Down:
                    direction = Vector3.down;
                    break;
            }

            isJoystickPushed = true;
        }

        public void Enter(GameManager owner)
        {
            isJoystickPushed = false;
        }

        public void Exit(GameManager owner)
        {
            player.NextPos = direction;
            isJoystickPushed = false;
        }

        public void Update(GameManager owner)
        {
            if (isJoystickPushed)
            {
                owner.Fsm.ChangeState(typeof(PlayState));
            }
        }
    }
}