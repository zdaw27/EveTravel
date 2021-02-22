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
        private WaitForSeconds waitSecond = new WaitForSeconds(2f);

        public override void Attack()
        {
            base.Attack();
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

        private void LevelUP()
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
            if(gameData.Exp >= 100)
            {
                LevelUP();
            }
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