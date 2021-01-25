using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfinding;

namespace EveTravel
{
    public class FSM<T>
    {
        private T owner;
        private IState<T> current;
        private Dictionary<Type, IState<T>> states = new Dictionary<Type, IState<T>>();

        public FSM(T owner, IState<T> entryState)
        {
            this.owner = owner;
            current = entryState;
            states.Add(entryState.GetType(), entryState);
        }

        public FSM<T> AddState<P>(P state) where P : IState<T>
        {
            states.Add(state.GetType(), state);
            return this;
        }

        public P GetState<P>()
        {
            return (P)states[typeof(P)];
        }

        public void ChangeState(Type type)
        {
            current.Exit(owner);
            current = states[type];
            current.Enter(owner);
        }

        public bool CheckCurrentState(Type type)
        {
            return current.GetType() == type;
        }

        public void StartFSM()
        {
            current.Enter(owner);
        }

        public void Update()
        {
            current.Update(owner);
        }
    }

    public interface IState<T>
    {
        void Enter(T owner);
        void Update(T owner);
        void Exit(T owner);
    }

    public class PlayerState : IState<GameManager>
    {
        NPC player;
        bool isJoystickPushed = false;
        Vector3 direction;

        public PlayerState(NPC player, UIObserver uiObserver)
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
        }

        public void Exit(GameManager owner)
        {
            isJoystickPushed = false;
        }

        public void Update(GameManager owner)
        {
            if (isJoystickPushed)
            {
                owner.Player.Fsm.GetState<MoveState>().nextPath = owner.Player.transform.position + direction;
                owner.Player.Fsm.ChangeState(typeof(MoveState));
                isJoystickPushed = false;
            }

            if (player.Fsm.CheckCurrentState(typeof(IdleState)))
            {
                owner.Fsm.ChangeState(typeof(ReadyState));
            }
        }
    }

    public class MonsterState : IState<GameManager>
    {
        public void Enter(GameManager owner)
        {
        }

        public void Exit(GameManager owner)
        {
        }

        public void Update(GameManager owner)
        {
            
        }
    }

    public class ReadyState : IState<GameManager>
    {
        public void Enter(GameManager owner)
        {
        }

        public void Exit(GameManager owner)
        {
        }

        public void Update(GameManager owner)
        {
            owner.Fsm.ChangeState(typeof(PlayerState));
        }
    }

    public class MoveState : IState<NPC>
    {
        public Vector3 nextPath { get; set; }
        bool isPathComplete = false;

        private void OnPathComplete(Path path)
        {
            nextPath = (Vector3)path.path[0].position;
            isPathComplete = true;
        }

        public void Enter(NPC owner)
        {
            isPathComplete = false;
            owner.Animator.Play("walk");
            if (nextPath.x - owner.transform.position.x > 0)
                owner.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            else if(nextPath.x - owner.transform.position.x < 0)
                owner.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            //owner.Seeker.StartPath(owner.transform.position, nextPath, OnPathComplete);
        }

        public void Update(NPC owner)
        {
            
            owner.transform.position = Vector3.MoveTowards(owner.transform.position, nextPath, Time.deltaTime * 2f);
            

            if(Vector3.Distance(owner.transform.position, nextPath) <= 0f)
            {
                owner.Fsm.ChangeState(typeof(IdleState));
            }
        }

        public void Exit(NPC owner)
        {
            isPathComplete = false;
        }
    }

    public class IdleState : IState<NPC>
    {

        public void Enter(NPC owner)
        {
            owner.Animator.Play("idle");
        }

        public void Update(NPC owner)
        {
        }

        public void Exit(NPC owner)
        {
        }
    }

    public class AttackState : IState<NPC>
    {
        public NPC attackTaget { get; set; }

        public void Enter(NPC owner)
        {
            owner.Animator.Play("attack");
            NPCStat stat = attackTaget.Stat;
            stat.hp -= owner.Stat.attack - stat.armor;
        }

        public void Update(NPC owner)
        {
        }

        public void Exit(NPC owner)
        {
        }
    }
}