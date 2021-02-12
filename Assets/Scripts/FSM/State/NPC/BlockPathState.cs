using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace EveTravel
{
    public class BlockPathState : IState<NPC>
    {
        private Path path;
        private bool isPathComplete;
        private NPC owner;

        BlockManager.TraversalProvider traversalProvider;

        public Path Path { get => path; set => path = value; }
        public bool IsPathComplete { get => isPathComplete; set => isPathComplete = value; }
        public BlockManager.TraversalProvider TraversalProvider { get => traversalProvider; set => traversalProvider = value; }

        public BlockPathState(NPC owner)
        {
            this.owner = owner;
            if (TraversalProvider == null)
            {
                TraversalProvider = new BlockManager.TraversalProvider(owner.Blocker.manager, BlockManager.BlockMode.AllExceptSelector, new List<SingleNodeBlocker>() { owner.Blocker });
            }
        }

        void OnPathComplete(Path path)
        {
            IsPathComplete = true;
            this.Path = path;
            if(Path.error)
            {
                Debug.LogWarning("Path Error");
                owner.NextPos = owner.transform.position;
            }
            else
                owner.NextPos = (Vector3)path.path[1].position;
        }

        public void Enter(NPC owner)
        {
            IsPathComplete = false;
            var path = ABPath.Construct(owner.transform.position, owner.GameData.Player.transform.position);

            path.traversalProvider = TraversalProvider;

            owner.Seeker.StartPath(path, OnPathComplete);
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