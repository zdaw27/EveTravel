using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class MoveState : IState<Character>
    {
        public void Enter(Character owner)
        {
            if (owner.Animator)
                owner.Animator.Play("walk");
            //if (owner.NextPos.x - owner.transform.position.x > 0)
            //    owner.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            //else if (owner.NextPos.x - owner.transform.position.x < 0)
            //    owner.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        public void Update(Character owner)
        {
            Vector3 move = Vector3.MoveTowards(owner.transform.position, owner.NextPos, Time.deltaTime * owner.GameData.NpcSpeed);
            owner.transform.position = move;

            if (owner.transform.position == owner.NextPos)
                owner.Fsm.ChangeState<IdleState>();
        }

        public void Exit(Character owner)
        {
        }
    }
}