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
        public bool IsIdle { get;  set; }

        virtual public void Attack()
        {
            if (animator)
                animator.CrossFadeInFixedTime("attack", 0.1f);

            int finalDamage = (stat.attack - attackTarget.stat.armor) <= 0 ? 0 : stat.attack - attackTarget.stat.armor;
            StartCoroutine(RotationSmoothly(attackTarget.transform.position - transform.position));
            effectListener.RaiseEffect(attackTarget.transform.position, EffectManager.EffectType.DamageEffect, finalDamage);
            attackTarget.stat.hp -= finalDamage;

            if (attackTarget.stat.hp <= 0)
                attackTarget.stat.hp = 0;

            if (animator)
                StartCoroutine(WaitForAttackAnim());
            else
                Idle();
        }

        private IEnumerator WaitForAttackAnim()
        {
            while (true)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f && animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
                {
                    break;
                }
                yield return null;
            }
            Idle();
        }

        public void Move()
        {
            if (animator)
                animator.CrossFadeInFixedTime("move", 0.1f);
            StartCoroutine(RotationSmoothly(NextPos - transform.position));
            StartCoroutine(MoveToNextPos());
            //if (owner.NextPos.x - owner.transform.position.x > 0)
            //    owner.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            //else if (owner.NextPos.x - owner.transform.position.x < 0)
            //    owner.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        public void Rotation(Vector3 dir)
        {
            StopCoroutine(RotationSmoothly(dir));
            StartCoroutine(RotationSmoothly(dir));
        }

        private IEnumerator RotationSmoothly(Vector3 dir)
        {
            float angle = Vector3.Angle(Vector3.up, dir.normalized);
            if (dir.x >= 0)
                angle *= -1f;
            while (true)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), 500f * Time.deltaTime);
                if(transform.rotation == Quaternion.Euler(0, 0, angle))
                {
                    break;
                }
                yield return null;
            }
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
                animator.CrossFadeInFixedTime("death", 0.1f);
                StartCoroutine(WaitForAnimationEnd());
            }
            else
                Destroy(gameObject);
        }

        public void Idle()
        {
            if (animator)
                animator.CrossFadeInFixedTime("idle", 0.1f);
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
                if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
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
