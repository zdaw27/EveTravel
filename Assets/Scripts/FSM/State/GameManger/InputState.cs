using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class InputState : IState<GameManager>
    {
        private bool isJoystickPushed = false;
        private bool isAttackButtonPushed = false;
        private Vector3 direction;
        private Dictionary<int, Enemy> enemyIndex = new Dictionary<int, Enemy>();
        private BaseEffect effect;
        private GameManager owner;

        public InputState(GameManager owner)
        {
            owner.UiObserver.OnJoyStick += OnJoystickDir;
            owner.UiObserver.OnAttakcButton += OnAttackClick;
        }

        ~InputState()
        {
            owner.UiObserver.OnJoyStick -= OnJoystickDir;
            owner.UiObserver.OnAttakcButton -= OnAttackClick;
        }

        private void OnAttackClick()
        {
            isAttackButtonPushed = true;
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
            direction = Vector3.zero;
            for (int i = 0; i < owner.GameData.Enemys.Count; ++i)
            {
                enemyIndex.Add(owner.GameData.EveMap.GetIndex(owner.GameData.Enemys[i].transform.position), owner.GameData.Enemys[i]);
            }
            isJoystickPushed = false;
            isAttackButtonPushed = false;
        }

        public void Exit(GameManager owner)
        {
            if (effect != null)
                effect.EndEffect();
        }

        public void Update(GameManager owner)
        {
            if (isJoystickPushed && owner.GameData.EveMap.CheckWalkablePosition(owner.GameData.Player.transform.position + direction) &&
                !enemyIndex.ContainsKey(owner.GameData.EveMap.GetIndex(owner.GameData.Player.transform.position + direction)))
            {
                owner.GameData.Player.SetNextPos(owner.GameData.Player.transform.position + direction);
                owner.GameData.Player.SetAttackTarget(null);
                owner.Fsm.ChangeState<BattleState>();
                return;
            }
            else if(isAttackButtonPushed && enemyIndex.ContainsKey(owner.GameData.EveMap.GetIndex(owner.GameData.Player.transform.position + direction)))
            {
                owner.GameData.Player.SetNextPos(owner.GameData.Player.transform.position);
                owner.GameData.Player.SetAttackTarget(enemyIndex[owner.GameData.EveMap.GetIndex(owner.GameData.Player.transform.position + direction)]);
                owner.Fsm.ChangeState<BattleState>();
                return;
            }

            if (isJoystickPushed && enemyIndex.ContainsKey(owner.GameData.EveMap.GetIndex(owner.GameData.Player.transform.position + direction)))
            {
                if (effect != null)
                    effect.EndEffect();
                effect = owner.EffectListener.RaiseEffect(owner.GameData.Player.transform.position + direction, EffectManager.EffectType.PermanentEffect);
            }

            isAttackButtonPushed = false;
            isJoystickPushed = false;
        }
    }
}