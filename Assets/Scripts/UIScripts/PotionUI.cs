using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EveTravel
{
    public class PotionUI : MonoBehaviour
    {
        [SerializeField] private GameData gameData;
        [SerializeField] private Text potionNumberText;
        [SerializeField] private GameEvent usePotionEvent;

        private void Awake()
        {
            potionNumberText.text = "0";
        }

        public void UpdatePotionNumberText()
        {
            potionNumberText.text = gameData.Potion.ToString();
        }

        public void UsePotionButtonClicked()
        {
            usePotionEvent.Raise();
        }

    }
}
