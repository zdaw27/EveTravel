using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace EveTravel
{
    public class PathState : IState<NPC>
    {
        private NPC player;
        private Path path;
        private bool isPathComplete;
        private NPC owner;

        void OnPathComplete(Path path)
        {
            isPathComplete = true;
            this.path = path;
            owner.NextPos = (Vector3)path.path[1].position;
        }

        public void Enter(NPC owner)
        {
            this.owner = owner;
            isPathComplete = false;
            owner.Seeker.StartPath(owner.transform.position, owner.GameData.Player.NextPos, OnPathComplete);
        }

        public void Exit(NPC owner)
        {
            isPathComplete = false;
        }

        public void Update(NPC owner)
        {
            if (isPathComplete)
            {
                owner.Fsm.ChangeState(typeof(MoveState));
            }
        }
    }
}