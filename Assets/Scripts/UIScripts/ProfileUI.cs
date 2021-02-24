using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

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
        [SerializeField]
        private Text hpText;
        [SerializeField]
        private Text expText;

        private StringBuilder sb = new StringBuilder();

        private void OnEnable()
        {
            if (gameData.Player)
            {
                UpdateExp();
                UpdateHp();
                UpdateGoldText();
                UpdateStat();
                PlayerLevelChanged();
            }
        }

        public void UpdateExp()
        {
            expBarImage.fillAmount = (float)gameData.Exp / 100f;
            sb.Clear();
            sb.Append(gameData.Exp.ToString()).Append(" / ").Append("100");
            expText.text = sb.ToString();
        }

        public void UpdateHp()
        {
            hpBarImage.fillAmount = (float)gameData.Player.Stat.hp / (float)gameData.Player.Stat.maxHp;
            sb.Clear();
            sb.Append(gameData.Player.Stat.hp.ToString()).Append(" / ").Append(gameData.Player.Stat.maxHp.ToString());
            hpText.text = sb.ToString();
        }

        public void UpdateGoldText()
        {
            goldText.text = inventory.Gold.ToString();
        }

        public void UpdateStat()
        {
            if (gameData.Equiped is null)
                attackValueText.text = gameData.Player.Stat.attack.ToString();
            else
                attackValueText.text = (gameData.Player.Stat.attack + gameData.Equiped.Attack).ToString();

            armorValueText.text = gameData.Player.Stat.armor.ToString();
        }

        public void PlayerLevelChanged()
        {
            levelText.text = gameData.PlayerLevel.ToString();
        }
    }
}