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

        public void AddButtonListener(UnityAction listenerCallBack)
        {
            GetComponent<Button>().onClick.AddListener(listenerCallBack);
        }

        public void RemoveSprite()
        {
            itemImage.sprite = null;
        }

        public void SetSprite(Sprite sprite)
        {
            itemImage.sprite = sprite;
        }
    }
}
