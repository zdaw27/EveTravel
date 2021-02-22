using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EveTravel
{
    public class ProfileUI : MonoBehaviour
    {
        [SerializeField]
        private GameData gameData;
        [SerializeField]
        private Inventory inventory;
        [SerializeField]
        private Image hpBarImage;
        [SerializeField]
        private Image expBarImage;
        [SerializeField]
        private Text goldText;
        [SerializeField]
        private Text levelText;
        [SerializeField]
        private Text attackValueText;
        [SerializeField]
        private Text armorValueText;


        public void Update()
        {
            hpBarImage.fillAmount = (float)gameData.Player.Stat.hp / (float)gameData.Player.Stat.maxHp;
            expBarImage.fillAmount = (float)gameData.Exp / 100f;
        }

        public void UpdateGoldText()
        {
            goldText.text = inventory.Gold.ToString();
        }

        public void UpdateStat()
        {
            attackValueText.text = gameData.Player.Stat.attack.ToString();
            armorValueText.text = gameData.Player.Stat.armor.ToString();
        }

        public void PlayerLevelChanged()
        {
            levelText.text = gameData.PlayerLevel.ToString();
        }
    }
}