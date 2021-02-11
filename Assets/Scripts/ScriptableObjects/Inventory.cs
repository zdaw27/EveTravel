using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects/Inventory", order = 1)]
public class Inventory : ScriptableObject
{
    Item Equiped { get; set; }
    List<Item> ItemContainer { get; set; } = new List<Item>();
}
