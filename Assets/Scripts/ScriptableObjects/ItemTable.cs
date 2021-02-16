using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemTable", menuName = "ScriptableObjects/ItemTable", order = 1)]
public class ItemTable : ScriptableObject
{
    [SerializeField] private List<Item> itemTables = null;

    public Item GetRandomItem()
    {
        return itemTables[Random.Range(0, itemTables.Count)];
    }
}
