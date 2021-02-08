using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class InputState : IState<GameManager>
    {
        GameData gameData;
        bool isJoystickPushed = false;
        Vector3 direction;

        public InputState(GameData gameData, UIObserver uiObserver)
        {
            uiObserver.OnJoyStick += OnJoystickDir;
            this.gameData = gameData;
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
            gameData.Player.NextPos = direction;
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