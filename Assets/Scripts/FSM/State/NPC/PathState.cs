using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace EveTravel
{
    public class PathState : IState<NPC>
    {
        private Path path;
        private bool isPathComplete;
        private NPC owner;

        public Path Path { get => path; set => path = value; }
        public bool IsPathComplete { get => isPathComplete; set => isPathComplete = value; }
        

        public PathState(NPC owner)
        {
            this.owner = owner;
        }

        void OnPathComplete(Path path)
        {
            IsPathComplete = true;
            this.Path = path;
            owner.NextPos = (Vector3)path.path[1].position;
            owner.Blocker.BlockAt((Vector3)path.path[1].position);
        }

        public void Enter(NPC owner)
        {
            owner.Blocker.Unblock();
            IsPathComplete = false;
            owner.Seeker.StartPath(owner.transform.position, owner.GameData.Player.NextPos , OnPathComplete);
        }

        public void Exit(NPC owner)
        {
            IsPathComplete = false;
        }

        public void Update(NPC owner)
        {
            //if (isPathComplete)
            //{
            //    owner.Fsm.ChangeState(typeof(MoveState));
            //}
        }
    }
}