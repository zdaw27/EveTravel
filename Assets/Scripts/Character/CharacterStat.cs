using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EveTravel
{
    [Serializable]
    public struct CharacterStat
    {
        [SerializeField]
        public int hp;
        [SerializeField]
        public int maxHp;
        [SerializeField]
        public int attack;
        [SerializeField]
        public int armor;

    }
}