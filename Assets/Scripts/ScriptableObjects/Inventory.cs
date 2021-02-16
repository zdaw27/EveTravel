using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects/Inventory", order = 1)]
public class Inventory : ScriptableObject
{
    [SerializeField] private List<Item> itemContainer = new List<Item>();

    Item Equiped { get; set; }
    
    public void AddItem(Item item)
    {
        itemContainer.Add(item);
    }

    public void DeleteItem(int itemIndex)
    {
        itemContainer.RemoveAt(itemIndex);
    }
}
