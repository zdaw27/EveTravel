using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EveTravel
{
    [Serializable]
    public class TileCell
    {
        [SerializeField]
        private TileType type;

        public TileType Type { get => type; set => type = value; }

        static public TileCell Create(TileType type)
        {
            TileCell newTile = new TileCell();
            newTile.Type = type;
            return newTile;
        }
    }
}