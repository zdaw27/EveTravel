using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class AttackState : IState<Character>
    {
        public void Enter(Character owner)
        {
            if (owner.Animator)
                owner.Animator.Play("attack");

            owner.Attack();
        }

        public void Update(Character owner)
        {
            owner.Fsm.ChangeState<IdleState>();
        }

        public void Exit(Character owner)
        {
            
        }
    }
}