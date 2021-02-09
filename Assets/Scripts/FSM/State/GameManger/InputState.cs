using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

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
            isJoystickPushed = false;
        }

        public void Update(GameManager owner)
        {
            int nodeIndex = AstarPath.active.GetNearest(gameData.Player.transform.position).node.NodeIndex;
            int nodeWidth = AstarPath.active.data.gridGraph.width;
            int nodeHeight = AstarPath.active.data.gridGraph.depth;
            int nextNodeIndex = nodeIndex + (int)direction.x + (int)direction.y * AstarPath.active.data.gridGraph.width;

            if (isJoystickPushed && gameData.EveMap.CheckWalkablePosition(gameData.Player.transform.position + direction))
            {
                Debug.Log("graph index" + AstarPath.active.GetNearest(gameData.Player.NextPos).node.NodeIndex + " tile index" + gameData.EveMap.GetIndex((int)gameData.Player.NextPos.x, (int)gameData.Player.NextPos.y));
                gameData.Player.NextPos = gameData.Player.transform.position + direction;
                owner.Fsm.ChangeState(typeof(PlayState));
            }
        }
    }
}