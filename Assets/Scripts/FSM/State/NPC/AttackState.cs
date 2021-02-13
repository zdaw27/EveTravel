using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class AttackState : IState<NPC>
    {
        public NPC attackTaget { get; set; }

        public void Enter(NPC owner)
        {
            if (owner.Animator)
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