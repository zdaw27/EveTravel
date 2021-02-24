using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace EveTravel
{
    public class Player : Character
    {
        [SerializeField]
        private GameEvent levelUpEvent;
        [SerializeField]
        private GameEvent playerLevelChangedEvent;
        [SerializeField]
        private EffectRaiser effectRaiser;
        //cache 
        private WaitForSeconds waitSecond = new WaitForSeconds(1.5f);

        public override void Attack()
        {
            if (animator)
                animator.CrossFadeInFixedTime("attack", 0.1f);

            int attack = gameData.Equiped is null ? stat.attack : stat.attack + gameData.Equiped.Attack;

            int finalDamage = (attack - attackTarget.Stat.armor) <= 0 ? 0 : attack - attackTarget.Stat.armor;
            StartCoroutine(RotationSmoothly(attackTarget.transform.position - transform.position));
            effectListener.RaiseEffect(attackTarget.transform.position, EffectManager.EffectType.DamageEffect, finalDamage);
            CharacterStat modifiedStat = attackTarget.Stat;
            modifiedStat.hp -= finalDamage;

            if (modifiedStat.hp <= 0)
                modifiedStat.hp = 0;

            attackTarget.Stat = modifiedStat;

            if (animator)
                StartCoroutine(WaitForAttackAnim());
            else
                Idle();
            effectListener.RaiseEffect(attackTarget.transform.position, EffectManager.EffectType.EnemyHit);
        }
        
        public void SetAttackTarget(Character attackTarget)
        {
            this.attackTarget = attackTarget;
        }

        public void SetNextPos(Vector3 nexPos)
        { 
            NextPos = nexPos;
        }

        public void LevelUP()
        {
            effectRaiser.RaiseEffect(transform.position, EffectManager.EffectType.LevelUpParticle);
            effectRaiser.RaiseEffect(transform.position, EffectManager.EffectType.LevelUpText);
            gameData.Exp = 0;
            gameData.PlayerLevel++;
            playerLevelChangedEvent.Raise();
            StartCoroutine(DelayRaise());
        }

        private IEnumerator DelayRaise()
        {
            yield return waitSecond;
            levelUpEvent.Raise();
        }

        public void EarnExp()
        {
            gameData.Exp += 10;
        }

        /// <summary>
        /// Player가 맵과 상호작용 로직.
        /// <para> example : Exit 입구 도착, 보물상자와 상호작용, 상점과 상호작용. </para>
        /// </summary>
        /// <param name="playerPos"></param>
        /// <returns> Exit에 도착했는가? </returns>
        public bool PlayerInteraction(EveMap eveMap)
        {
            int index = eveMap.GetIndex(transform.position);

            if (eveMap.TreasureIndex.Contains(index))
            {
                eveMap.Treasures[index].OpenTreasure(this);
                eveMap.TreasureIndex.Remove(index);
            }

            if (eveMap.ExitIndex.Contains(index))
                return true;
            else
                return false;
        }

        
    }
}