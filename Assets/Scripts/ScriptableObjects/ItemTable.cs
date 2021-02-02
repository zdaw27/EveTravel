using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemTable", menuName = "ScriptableObjects/ItemTable", order = 1)]
public class ItemTable : ScriptableObject
{
    [SerializeField] private List<Item> gradeOneTable = null;
    [SerializeField] private List<Item> gradeTwoTable = null;
}
