using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EveTravel
{
    public class FSM
    {
        private NPC owner;
        private IState entry;

        public FSM(NPC owner, IState entryState)
        {
            this.owner = owner;
            entry = entryState;
        }

        public void StartFSM()
        {
            entry.Enter(owner);
        }

        public void Update()
        {
        }
    }

    public interface IState
    {
        void Enter(NPC owner);
        void Update(NPC owner);
        void Exit(NPC owner);
    }

    public class MoveState : IState
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

    public class IdleState : IState
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

    public class AttackState : IState
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