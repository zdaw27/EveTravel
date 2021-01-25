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

        public FSM(T owner, IState<T> entryState)
        {
            this.owner = owner;
            current = entryState;
        }

        public void ChangeState(IState<T> nextState)
        {
            current.Exit(owner);
            current = nextState;
            current.Enter(owner);
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
        JoyStickDir direction;

        public PlayerState(NPC player, Action<JoyStickDir> onJoystickDir)
        {
            onJoystickDir += OnJoystickDir;
            this.player = player;
        }

        private void OnJoystickDir(JoyStickDir dir)
        {
            direction = dir;
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
            if(isJoystickPushed)
            {
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

    public class BattleState : IState<GameManager>
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

    public class PathState : IState<NPC>
    {
        public Vector2 targetPos { get; set; }
        bool pathComplete = false;
        NPC npc;

        private void OnPathComplete(Path path)
        {
            pathComplete = true;
            npc.path = path;
        }

        public void Enter(NPC owner)
        {
            npc = owner;
            owner.Animator.Play("walk");
            //find Path
            owner.seeker.StartPath(owner.transform.position, targetPos, OnPathComplete);
        }

        public void Update(NPC owner)
        {
        }

        public void Exit(NPC owner)
        {
            pathComplete = false;
        }
    }

    public class MoveState : IState<NPC>
    {
        public Vector2 targetPos { get; set; }

        public void Enter(NPC owner)
        {
            owner.Animator.Play("walk");
        }

        public void Update(NPC owner)
        {
            Vector2 pos2D = owner.transform.position;
            if (targetPos != pos2D)
            {
                Vector2 dir = targetPos - pos2D;
                dir.Normalize();
                owner.transform.position = Vector3.MoveTowards(owner.transform.position, targetPos, Time.deltaTime * 2f);
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