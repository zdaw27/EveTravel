using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EveTravel
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField]
        private Inventory inventory;
        [SerializeField]
        private GameObject equipUI;
        [SerializeField]
        private Text equipUIAttackValue;
        [SerializeField]
        private Image equipUIItemIamge;
        [SerializeField]
        private GameObject unequipUI;
        [SerializeField]
        private List<ItemCellUI> itemCellUIs = new List<ItemCellUI>();
        [SerializeField]
        private GameData gameData;
        [SerializeField]
        private Transform itemCellParent;
        [SerializeField]
        private Image equipedImage;
        [SerializeField]
        private GameEvent statChangedEvent;

        private int itemIndex = 0;

        private void Awake()
        {
            itemCellUIs.Clear();
            foreach (Transform child in itemCellParent)
            {
                itemCellUIs.Add(child.GetComponent<ItemCellUI>());
            }
            

            for (int i = 0; i < itemCellUIs.Count; ++i)
            {
                int closerIndex = i;
                itemCellUIs[i].AddButtonListener(delegate { OpenEquipUI(closerIndex); });
            }
        }

        private void OnEnable()
        {
            UpdateInventoryUI();
        }

        public void UpdateInventoryUI()
        {
            if (gameData.Equiped is null)
                equipedImage.enabled = false;
            else
                equipedImage.enabled = true;

            for (int i = 0; i < itemCellUIs.Count; ++i)
            {
                itemCellUIs[i].DisableCell();
            }

            int itemIndex = 0;
            foreach (Item item in inventory)
            {
                itemCellUIs[itemIndex].EnableCell(item.ItemSprite);
                itemIndex++;
            }
        }

        public void QuitInventory()
        {
            gameObject.SetActive(false);
        }

        public void OpenEquipUI(int itemIndex)
        {
            this.itemIndex = itemIndex;
            equipUIItemIamge.sprite = inventory.GetItem(itemIndex).ItemSprite;
            equipUIAttackValue.text = inventory.GetItem(itemIndex).Attack.ToString();
            equipUI.SetActive(true);
        }

        public void EquipUIConfirm()
        {
            gameData.Equiped = inventory.GetItem(itemIndex);
            inventory.DeleteItem(itemIndex);

            equipedImage.enabled = true;
            equipedImage.sprite = gameData.Equiped.ItemSprite;
            
            equipUI.SetActive(false);
            UpdateInventoryUI();
            statChangedEvent.Raise();
        }

        public void EquipUICancel()
        {
            equipUI.SetActive(false);
        }

        public void OpenUnEquipUI()
        {
            if(gameData.Equiped != null)
                unequipUI.SetActive(true);
        }

        public void UnEquipConfirm()
        {
            equipedImage.sprite = null;
            equipedImage.enabled = false;

            inventory.AddItem(gameData.Equiped);
            gameData.Equiped = null;
            unequipUI.SetActive(false);
            UpdateInventoryUI();
            statChangedEvent.Raise();
        }

        public void UnEquipCancel()
        {
            equipUI.SetActive(false);
        }
    }
}