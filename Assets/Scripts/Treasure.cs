using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class Treasure : MonoBehaviour
    {
        public enum LootType
        {
            Item = 0,
            Gold,
            Potion
        }

        [SerializeField]
        private Inventory inventory;
        [SerializeField]
        private ItemTable itemTable;
        [SerializeField]
        private EffectRaiser effectRaiser;

        public void OpenTreasure(Player player)
        {
            LootType lootType = (LootType)Random.Range(0,3);

            switch (lootType)
            {
                case LootType.Item:
                    //inventory.AddItem(itemTable.GetRandomItem());
                    effectRaiser.RaiseEffect(player.transform.position, EffectManager.EffectType.LootingEffect_Weapon);
                    effectRaiser.RaiseEffect(player.transform.position, EffectManager.EffectType.LootingEffect_Dust);
                    break;
                case LootType.Gold:
                    inventory.AddGoldRandom();
                    effectRaiser.RaiseEffect(player.transform.position, EffectManager.EffectType.CoinEffect);
                    break;
                case LootType.Potion:
                    effectRaiser.RaiseEffect(player.transform.position, EffectManager.EffectType.LootingEffect_Potion);
                    effectRaiser.RaiseEffect(player.transform.position, EffectManager.EffectType.LootingEffect_Dust);
                    inventory.AddPotion(1);
                    break;
            }

            GameObject.Destroy(gameObject);
        }
    }
}