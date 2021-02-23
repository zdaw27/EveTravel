using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace EveTravel
{
    public class Enemy : Character
    {
        [SerializeField]
        private EffectRaiser effectRaiser;
        [SerializeField]
        private Inventory inventory;
        [SerializeField]
        private float aggroRange = 5f;

        public float AggroRange { get => aggroRange; private set => aggroRange = value; }

        public override void Attack()
        {
            base.Attack();
            effectListener.RaiseEffect(attackTarget.transform.position, EffectManager.EffectType.PlayerHit);
        }

        public void GetNextPos()
        {
            gameData.EveMap.GetNextPos(this);
        }

        public void GetAttackTarget()
        {
            if (Vector3.Distance(GameData.Player.transform.position, transform.position) <= 1f)
                attackTarget = GameData.Player;
            else
                attackTarget = null;
        }

        public override void Death()
        {
            base.Death();
            inventory.AddGoldRandom();
            effectRaiser.RaiseEffect(transform.position, EffectManager.EffectType.CoinEffect);
        }
    }
}