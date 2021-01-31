using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfinding;

namespace EveTravel
{
    public class FSM<T> where T : MonoBehaviour
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
            Debug.Log(owner.name + current.ToString());
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

    public class PlayState : IState<GameManager>
    {
        public void Enter(GameManager owner)
        {
            owner.Player.NextPos = owner.Player.transform.position + owner.Player.NextPos;
            owner.Player.Fsm.ChangeState(typeof(MoveState));
            
            owner.Enemy.Fsm.ChangeState(typeof(PathState));
            
        }

        public void Exit(GameManager owner)
        {
        }

        public void Update(GameManager owner)
        {
            if (owner.Player.Fsm.CheckCurrentState(typeof(IdleState)) && owner.Enemy.Fsm.CheckCurrentState(typeof(IdleState)))
            {
                owner.Fsm.ChangeState(typeof(ReadyState));
            }
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
            owner.Fsm.ChangeState(typeof(InputState));
        }
    }

    public class MoveState : IState<NPC>
    {
        public void Enter(NPC owner)
        {
            owner.Animator.Play("walk");
            if (owner.NextPos.x - owner.transform.position.x > 0)
                owner.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            else if(owner.NextPos.x - owner.transform.position.x < 0)
                owner.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            //owner.Seeker.StartPath(owner.transform.position, nextPath, OnPathComplete);
        }

        public void Update(NPC owner)
        {
            Vector3 move = Vector3.MoveTowards(owner.transform.position, owner.NextPos, Time.deltaTime * owner.GameData.NpcSpeed);
            Debug.Log(move);
            owner.transform.position = move;

            if(Vector3.Distance(owner.transform.position, owner.NextPos) <= 0f)
            {
                owner.Fsm.ChangeState(typeof(IdleState));
            }
        }

        public void Exit(NPC owner)
        {
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

    public class PathState : IState<NPC>
    {
        private NPC player;
        private Path path;
        private bool isPathComplete;
        private NPC owner;

        void OnPathComplete(Path path)
        {
            isPathComplete = true;
            this.path = path;
            owner.NextPos = (Vector3)path.path[1].position;
        }

        public void Enter(NPC owner)
        {
            this.owner = owner;
            isPathComplete = false;
            owner.Seeker.StartPath(owner.transform.position, owner.GameData.Player.NextPos, OnPathComplete);
        }

        public void Exit(NPC owner)
        {
            isPathComplete = false;
        }

        public void Update(NPC owner)
        {
            if(isPathComplete)
            {
                owner.Fsm.ChangeState(typeof(MoveState));
            }
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