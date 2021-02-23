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
        private GameObject unequipUI;
        [SerializeField]
        private Image equipUIItemIamge;
        [SerializeField]
        private List<ItemCellUI> itemCellUIs;
        [SerializeField]
        private GameData gameData;

        private int itemIndex = 0;

        private void Awake()
        {
            for (int i = 0; i < itemCellUIs.Count; ++i)
            {
                int closerIndex = i;
                itemCellUIs[i].AddButtonListener(delegate { OpenEquipUI(i); });
            }
        }

        public void UpdateInventory()
        {
            for (int i = 0; i < itemCellUIs.Count; ++i)
                itemCellUIs[i].RemoveSprite();

            int itemIndex = 0;
            foreach (Item item in inventory)
            {
                itemCellUIs[itemIndex].SetSprite(item.ItemSprite);
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
            equipUI.SetActive(true);
        }

        public void EquipUIConfirm()
        {
            gameData.Equiped = inventory.GetItem(itemIndex);
            equipUI.SetActive(false);
        }

        public void EquipUICancel()
        {
            equipUI.SetActive(false);
        }

        public void OpenUnEquipUI()
        {
            unequipUI.SetActive(true);
        }

        public void UnEquipConfirm()
        {
            gameData.Equiped = null;
            unequipUI.SetActive(false);
        }

        public void UnEquipCancel()
        {
            equipUI.SetActive(false);
        }
    }
}