using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class DeathState : IState<Character>
    {
        public void Enter(Character owner)
        {
            if (owner.Animator)
                owner.Animator.Play("Death");
        }

        public void Update(Character owner)
        {
            
        }

        public void Exit(Character owner)
        {
        }
    }
}