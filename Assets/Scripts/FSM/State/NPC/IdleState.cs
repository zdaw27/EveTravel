using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class IdleState : IState<Character>
    {
        public void Enter(Character owner)
        {
            if (owner.Animator)
                owner.Animator.Play("idle");
        }

        public void Update(Character owner)
        {
        }

        public void Exit(Character owner)
        {
        }
    }
}