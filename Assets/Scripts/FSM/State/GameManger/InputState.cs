using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class InputState : IState<GameManager>
    {
        private GameData gameData;
        private UIObserver uiObserver;
        private EffectListener effectListener;
        private bool isJoystickPushed = false;
        private bool isAttackButtonPushed = false;
        private Vector3 direction;
        private Dictionary<int, Enemy> enemyIndex = new Dictionary<int, Enemy>();
        private BaseEffect effect;

        public InputState(GameData gameData, UIObserver uiObserver, EffectListener effectListener)
        {
            uiObserver.OnJoyStick += OnJoystickDir;
            uiObserver.OnAttakcButton += OnAttackClick;
            this.gameData = gameData;
            this.uiObserver = uiObserver;
            this.effectListener = effectListener;
        }

        ~InputState()
        {
            uiObserver.OnJoyStick -= OnJoystickDir;
            uiObserver.OnAttakcButton -= OnAttackClick;
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
            if (isJoystickPushed && gameData.EveMap.CheckWalkablePosition(gameData.Player.transform.position + direction) &&
                !enemyIndex.ContainsKey(gameData.EveMap.GetIndex(gameData.Player.transform.position + direction)))
            {
                gameData.Player.SetNextPos(gameData.Player.transform.position + direction);
                gameData.Player.SetAttackTarget(null);
                owner.Fsm.ChangeState<NextStepState>();

            }
            else if(isAttackButtonPushed && enemyIndex.ContainsKey(gameData.EveMap.GetIndex(gameData.Player.transform.position + direction)))
            {
                gameData.Player.SetNextPos(gameData.Player.transform.position);
                gameData.Player.SetAttackTarget(enemyIndex[gameData.EveMap.GetIndex(gameData.Player.transform.position + direction)]);
                owner.Fsm.ChangeState<NextStepState>();
            }

            if (isJoystickPushed && enemyIndex.ContainsKey(gameData.EveMap.GetIndex(gameData.Player.transform.position + direction)))
            {
                if (effect != null)
                    effect.EndEffect();
                effect = effectListener.RaiseEffect(gameData.Player.transform.position + direction, EffectManager.EffectType.PermanentEffect);
            }

            isAttackButtonPushed = false;
            isJoystickPushed = false;
        }
    }
}