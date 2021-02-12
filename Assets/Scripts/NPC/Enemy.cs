using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace EveTravel
{
    public class Enemy : NPC
    {
        private void Awake()
        {
            fsm = new FSM<NPC>(this, new IdleState());
            fsm.AddState(new PathState(this));
            fsm.AddState(new BlockPathState(this));
            fsm.AddState(new MoveState());
        }

        // Start is called before the first frame update
        void Start()
        {
            fsm.StartFSM();
        }

        // Update is called once per frame
        void Update()
        {
            fsm.Update();
        }
    }
}