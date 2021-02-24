using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

namespace EveTravel
{
    public class ItemCellUI : MonoBehaviour
    {
        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private Button cellButton;

        public void AddButtonListener(UnityAction listenerCallBack)
        {
            GetComponent<Button>().onClick.AddListener(listenerCallBack);
        }

        public void DisableCell()
        {
            itemImage.enabled = false;
            cellButton.enabled = false;
        }

        public void EnableCell(Sprite sprite)
        {
            itemImage.enabled = true;
            cellButton.enabled = true;
            itemImage.sprite = sprite;
        }
    }
}
