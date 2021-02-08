using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class IdleState : IState<NPC>
    {
        public void Enter(NPC owner)
        {
            if (owner.Animator)
                owner.Animator.Play("idle");
        }

        public void Update(NPC owner)
        {
        }

        public void Exit(NPC owner)
        {
        }
    }
}