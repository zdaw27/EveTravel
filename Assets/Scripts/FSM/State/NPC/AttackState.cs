using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class AttackState : IState<Character>
    {
        public void Enter(Character owner)
        {
            owner.Attack();
        }

        public void Update(Character owner)
        {
        }

        public void Exit(Character owner)
        {
            
        }
    }
}