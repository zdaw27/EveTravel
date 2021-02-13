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
        private HashSet<int> enemyIndex = new HashSet<int>();

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
            enemyIndex.Clear();
            for(int i = 0; i < owner.GameData.Enemys.Count; ++i)
            {
                enemyIndex.Add(owner.GameData.EveMap.GetIndex(owner.GameData.Enemys[i].transform.position));
            }
            isJoystickPushed = false;
        }

        public void Exit(GameManager owner)
        {
            isJoystickPushed = false;
        }

        public void Update(GameManager owner)
        {
            if (isJoystickPushed && gameData.EveMap.CheckWalkablePosition(gameData.Player.transform.position + direction) &&
                !enemyIndex.Contains(gameData.EveMap.GetIndex(gameData.Player.transform.position + direction)))
            {
                gameData.Player.NextPos = gameData.Player.transform.position + direction;
                owner.Fsm.ChangeState(typeof(PathFindState));
            }
        }
    }
}