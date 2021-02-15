using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EveTravel
{
    [Serializable]
    public enum TileType
    {
        walkable,
        noneWlakable
    }

    public class EveMap : MonoBehaviour
    {
        [SerializeField] private SerializableIntHashSet playerSpawnIndex;
        [SerializeField] private SerializableIntHashSet enemySpawnIndex;
        [SerializeField] private SerializableIntHashSet treasureIndex;
        [SerializeField] private SerializableIntHashSet exitIndex;
        [SerializeField] private SerializableIntHashSet storeIndex;
        [SerializeField] private int width;
        [SerializeField] private int height;
        [SerializeField] private List<TileCell> tileCell;
        [SerializeField] private GameData gameData;

        private readonly List<Vector3> checkList = new List<Vector3>() { Vector3.left, Vector3.up, Vector3.right, Vector3.down };
        //패스 계산중에 완료된 목록 저장용도.
        private HashSet<int> walkedIndices = new HashSet<int>();
        private List<Vector3> tileCandidate = new List<Vector3>();

        public SerializableIntHashSet PlayerSpawnIndex { get => playerSpawnIndex; set => playerSpawnIndex = value; }
        public SerializableIntHashSet EnemySpawnIndex { get => enemySpawnIndex; set => enemySpawnIndex = value; }
        public SerializableIntHashSet TreasureIndex { get => treasureIndex; set => treasureIndex = value; }
        public SerializableIntHashSet ExitIndex { get => exitIndex; set => exitIndex = value; }
        public SerializableIntHashSet StoreIndex { get => storeIndex; set => storeIndex = value; }
        public List<TileCell> TileCell { get => tileCell; set => tileCell = value; }
        public int Height { get => height; set => height = value; }
        public int Width { get => width; set => width = value; }


        public bool CheckWalkablePosition(Vector3 position)
        {
            int index = GetIndex(position);
            return IsInRange(position) && tileCell[index].Type == TileType.walkable;
        }

        public bool IsInRange(Vector3 position)
        {
            return position.x >= 0 && position.x < width && position.y >= 0 && position.y < height;
        }

        public int GetIndex(Vector3 pos)
        {
            return (int)pos.x + (int)pos.y * width;
        }

        public void GetNextPos(Character character)
        {
            tileCandidate.Clear();
            character.NextPos = character.transform.position;

            if(Vector3.Distance(character.transform.position, gameData.Player.NextPos) <= 1f || Vector3.Distance(character.transform.position, gameData.Player.transform.position) <= 1f)
            {
                character.NextPos = character.transform.position;
                walkedIndices.Add(GetIndex(character.NextPos));
                return;
            }

            for (int i = 0; i < checkList.Count; ++i)
            {
                if (CheckWalkablePosition(character.transform.position + checkList[i])
                    && EveUtil.IsIn180Angle(character.transform.position, gameData.Player.NextPos, character.transform.position + checkList[i]))
                {
                    tileCandidate.Add(character.transform.position + checkList[i]);
                }
            }

            tileCandidate.Sort((Vector3 a, Vector3 b) =>
            {
                if (Vector3.Distance(a, gameData.Player.NextPos) < Vector3.Distance(b, gameData.Player.NextPos))
                {
                    return -1;
                }
                else if (Vector3.Distance(a, gameData.Player.NextPos) > Vector3.Distance(b, gameData.Player.NextPos))
                    return 1;
                else
                    return 0;
            });

            for (int i = 0; i < tileCandidate.Count; ++i)
            {
                if (!walkedIndices.Contains(GetIndex(tileCandidate[i])))
                {
                    character.NextPos = tileCandidate[i];
                    walkedIndices.Add(GetIndex(character.NextPos));
                    return;
                }
            }

            if (character.NextPos == character.transform.position)
            {
                walkedIndices.Add(GetIndex(character.NextPos));
            }
        }

        /// <summary>
        /// Character 가 어느지점에 도착할지 기억해 놓는 인덱스 초기화.
        /// </summary>
        public void ClearWakedIndices()
        {
            walkedIndices.Clear();
        }
    }
}