using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item
{
    [SerializeField]
    private int attack;
    [SerializeField]
    private Sprite itemSprite;

    public int Attack { get => attack; set => attack = value; }
    public Sprite ItemSprite { get => itemSprite; set => itemSprite = value; }

    public Item ShallowCopy()
    {
        return (Item)this.MemberwiseClone();
    }
}