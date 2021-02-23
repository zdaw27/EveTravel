using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects/Inventory", order = 1)]
    public class Inventory : ScriptableObject, IEnumerable
    {
        [SerializeField]
        private List<Item> itemContainer = new List<Item>();
        [SerializeField]
        private GameEvent goldChangeEvent;
        [SerializeField]
        private GameEvent potionCountChangeEvent;
        [SerializeField]
        private ItemTable itemTable;

        public Item Equiped { get; set; }
        public int Potion { get; set; }
        public int Gold { get; set; }
        public bool IsFull {
            get {
                if (itemContainer.Count > 12)
                    return true;
                return false ;
            }
        }

        private void OnEnable()
        {
            ResetData();
        }

        public void ResetData()
        {
            itemContainer.Clear();
            Equiped = null;
            Potion = 5;
            Gold = 0;
            //debug
            itemContainer.Add(itemTable.GetRandomItem());
        }

        public Item GetItem(int index)
        {
            return itemContainer[index];
        }

        public void AddItem(Item item)
        {
            itemContainer.Add(item);
        }

        public void AddPotion(int quantity)
        {
            Potion += quantity;
            potionCountChangeEvent.Raise();
        }

        public void DeleteItem(int itemIndex)
        {
            itemContainer.RemoveAt(itemIndex);
        }

        public void AddGoldRandom()
        {
            int goldAmount = Random.Range(10, 20);
            Gold += goldAmount;
            goldChangeEvent.Raise();
        }

        public IEnumerator GetEnumerator()
        {
            return itemContainer.GetEnumerator();
        }
    }
}