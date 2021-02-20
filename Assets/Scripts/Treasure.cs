using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public enum LootType
    {
        Item = 0,
        Gold,
        Potion
    }

    [SerializeField] private Inventory inventory;
    [SerializeField] private ItemTable itemTable;

    private LootType lootType;

    public void Looting()
    {
        lootType = (LootType)Random.Range(0, 3);
        inventory.AddItem(itemTable.GetRandomItem());

        switch (lootType)
        {
            case LootType.Item:
                break;
            case LootType.Gold:
                break;
            case LootType.Potion:
                break;
            default:
                break;
        }
    }

}
