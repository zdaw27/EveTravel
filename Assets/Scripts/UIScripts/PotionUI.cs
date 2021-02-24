using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EveTravel
{
    public class PotionUI : MonoBehaviour
    {
        [SerializeField]
        private Inventory inventory;
        [SerializeField]
        private Text potionNumberText;
        [SerializeField]
        private GameEvent usePotionEvent;

        private void OnEnable()
        {
            potionNumberText.text = inventory.Potion.ToString();
        }

        private void Awake()
        {
            potionNumberText.text = inventory.Potion.ToString();
        }

        public void UpdatePotionNumberText()
        {
            potionNumberText.text = inventory.Potion.ToString();
        }

        public void UsePotionButtonClicked()
        {
            usePotionEvent.Raise();
        }

    }
}
