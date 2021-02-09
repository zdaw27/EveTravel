using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace EveTravel
{
    [RequireComponent(typeof(BlockManager))]
    public class BlockManagerSetter : MonoBehaviour
    {
        [SerializeField] private GameData gameData;

        protected void OnEnable()
        {
            gameData.BlockManager = GetComponent<BlockManager>();
        }

        protected void OnDisable()
        {
            gameData.BlockManager = null;
        }
    }
}