using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class Enemy : NPC
    {
        private void Awake()
        {
            fsm = new FSM<NPC>(this, new IdleState());
            fsm.AddState(new MoveState());
        }
    }
}