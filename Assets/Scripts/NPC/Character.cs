using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] protected GameData gameData;
        [SerializeField] protected CharacterStat stat;
        [SerializeField] protected Animator animator;
        [SerializeField] protected EffectListener effectListener;
        [SerializeField] protected EveMap evemap;

        protected Character attackTarget;

        public Animator Animator { get { return animator; } }
        public CharacterStat Stat { get { return stat; } set { stat = value; } }
        public Vector3 NextPos { get; set; }
        public GameData GameData { get { return gameData; } private set { } }
        public bool IsIdle { get; private set; }

        virtual public void Attack()
        {
            if (animator)
                animator.Play("attack");

            int finalDamage = (stat.attack - attackTarget.stat.armor) <= 0 ? 0 : stat.attack - attackTarget.stat.armor;

            effectListener.RaiseEffect(attackTarget.transform.position, EffectManager.EffectType.DamageEffect, finalDamage);
            attackTarget.stat.hp -= finalDamage;

            if (attackTarget.stat.hp <= 0)
                attackTarget.stat.hp = 0;

            Idle();
        }

        public void Move()
        {
            if (animator)
                animator.Play("walk");

            StartCoroutine(MoveToNextPos());
            //if (owner.NextPos.x - owner.transform.position.x > 0)
            //    owner.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            //else if (owner.NextPos.x - owner.transform.position.x < 0)
            //    owner.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        private IEnumerator MoveToNextPos()
        {
            while(true)
            {
                Vector3 move = Vector3.MoveTowards(transform.position, NextPos, Time.deltaTime * GameData.NpcSpeed);
                transform.position = move;

                if (transform.position == NextPos)
                {
                    Idle();
                    break;
                }
                yield return null;
            }
        }

        public void Death()
        {
            if (animator)
            {
                animator.Play("death");
                StartCoroutine(WaitForAnimationEnd());
            }
            else
                Destroy(gameObject);
        }

        private void Idle()
        {
            if (animator)
                animator.Play("idle");
            IsIdle = true;
        }

        public void ResetIdleState()
        {
            IsIdle = false;
        }

        private IEnumerator WaitForAnimationEnd()
        {
            while(true)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                    break;
                yield return null;
            }

            Destroy(gameObject);
        }

        virtual public bool HasTarget()
        {
            return !(attackTarget is null);
        }

        public bool CheckCurrentAnimStateName(string animationName)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
                return true;
            return false;
        }
    }
}
