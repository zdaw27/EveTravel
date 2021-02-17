using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private ItemTable itemTable;

    public void Looting()
    {
        inventory.AddItem(itemTable.GetRandomItem());
    }

}
