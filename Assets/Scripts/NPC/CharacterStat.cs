using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EveTravel
{
    [Serializable]
    public struct CharacterStat
    {
        [SerializeField] public Vector2 lastPosition; 
        [SerializeField] public int hp;
        [SerializeField] public int attack;
        [SerializeField] public int armor;
        [SerializeField] public int critical;
    }
}