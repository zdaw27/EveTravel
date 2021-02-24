using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class InputState : IState<GameManager>
    {
        private Dictionary<int, Enemy> enemyIndex = new Dictionary<int, Enemy>();

        public void Enter(GameManager owner)
        {
            enemyIndex.Clear();
            for (int i = 0; i < owner.GameData.Enemys.Count; ++i)
            {
                enemyIndex.Add(owner.GameData.EveMap.GetIndex(owner.GameData.Enemys[i].transform.position), owner.GameData.Enemys[i]);
            }
        }

        public void Exit(GameManager owner)
        {
        }

        public void Update(GameManager owner)
        {
            Vector3 direction = Vector3.zero;

            switch (owner.JoystickObserver.JoyStickDir)
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
                case JoyStickDir.None:
                    direction = Vector3.zero;
                    break;
            }

            if (owner.CheckJoystickPushed() && owner.GameData.EveMap.CheckWalkablePosition(owner.GameData.Player.transform.position + direction) &&
                !enemyIndex.ContainsKey(owner.GameData.EveMap.GetIndex(owner.GameData.Player.transform.position + direction)))
            {
                owner.GameData.Player.SetNextPos(owner.GameData.Player.transform.position + direction);
                owner.GameData.Player.SetAttackTarget(null);
                owner.RemoveAim();
                owner.Fsm.ChangeState<BattleState>();
                return;
            }
            else if(owner.IsAttackButtonPushed && enemyIndex.ContainsKey(owner.GameData.EveMap.GetIndex(owner.GetAimPos())))
            {
                owner.GameData.Player.SetNextPos(owner.GameData.Player.transform.position);
                owner.GameData.Player.SetAttackTarget(enemyIndex[owner.GameData.EveMap.GetIndex(owner.GetAimPos())]);
                owner.Fsm.ChangeState<BattleState>(); 
                return;
            }

            if (owner.CheckJoystickPushed() && enemyIndex.ContainsKey(owner.GameData.EveMap.GetIndex(owner.GameData.Player.transform.position + direction)))
            {
                owner.SetAim(owner.GameData.Player.transform.position + direction);
            }
        }
    }
}