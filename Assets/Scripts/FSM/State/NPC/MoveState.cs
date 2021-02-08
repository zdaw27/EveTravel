using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace EveTravel
{
    public class MoveState : IState<NPC>
    {
        public void Enter(NPC owner)
        {
            if (owner.Animator)
                owner.Animator.Play("walk");
            if (owner.NextPos.x - owner.transform.position.x > 0)
                owner.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            else if (owner.NextPos.x - owner.transform.position.x < 0)
                owner.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        public void Update(NPC owner)
        {
            Vector3 move = Vector3.MoveTowards(owner.transform.position, owner.NextPos, Time.deltaTime * owner.GameData.NpcSpeed);
            Debug.Log(move);
            owner.transform.position = move;

            if (Vector3.Distance(owner.transform.position, owner.NextPos) <= 0f)
            {
                owner.Fsm.ChangeState(typeof(IdleState));
            }
        }

        public void Exit(NPC owner)
        {
        }
    }
}