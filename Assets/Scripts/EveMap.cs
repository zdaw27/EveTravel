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
        [SerializeField]
        private SerializableIntHashSet playerSpawnIndex;
        [SerializeField]
        private SerializableIntHashSet enemySpawnIndex;
        [SerializeField]
        private SerializableIntHashSet treasureIndex;
        [SerializeField]
        private SerializableIntHashSet exitIndex;
        [SerializeField]
        private SerializableIntHashSet storeIndex;
        [SerializeField]
        private int width;
        [SerializeField]
        private int height;
        [SerializeField]
        private List<TileCell> tileCell;
        [SerializeField]
        private GameData gameData;
        [Header("Prefabs")]
        [SerializeField]
        private GameObject playerPrefab;
        [SerializeField]
        private GameObject enemyPrefab;
        [SerializeField]
        private GameObject treasurePrefab;
        [SerializeField]
        private GameObject exitPrefab;
        [Header("Config")]
        [SerializeField]
        private bool createPlayer = false;
        [SerializeField]
        private bool createEnemies = false;
        [SerializeField]
        private bool createTreasures = false;
        [SerializeField]
        private bool createExit = false;

        #region PathFinding
        private readonly List<Vector3> checkList = new List<Vector3>() { Vector3.left, Vector3.up, Vector3.right, Vector3.down };
        //패스 계산중에 완료된 목록 저장용도.
        private HashSet<int> walkedIndices = new HashSet<int>();
        private List<Vector3> tileCandidate = new List<Vector3>();
        #endregion

        private Dictionary<int, Treasure> treasures = new Dictionary<int, Treasure>();
        
        public List<TileCell> TileCell { get => tileCell; set => tileCell = value; }
        public int Height { get => height; set => height = value; }
        public int Width { get => width; set => width = value; }
        public SerializableIntHashSet PlayerSpawnIndex { get => playerSpawnIndex; set => playerSpawnIndex = value; }
        public SerializableIntHashSet EnemySpawnIndex { get => enemySpawnIndex; set => enemySpawnIndex = value; }
        public SerializableIntHashSet TreasureIndex { get => treasureIndex; set => treasureIndex = value; }
        public SerializableIntHashSet ExitIndex { get => exitIndex; set => exitIndex = value; }
        public SerializableIntHashSet StoreIndex { get => storeIndex; set => storeIndex = value; }
        public Dictionary<int, Treasure> Treasures { get => treasures; set => treasures = value; }

        private void Awake()
        {
            CreateMapObjects();
        }

        private void CreateMapObjects()
        {
            int maxIndex = width * Height;
            for (int i = 0; i < maxIndex; ++i)
            {
                if (createPlayer && PlayerSpawnIndex.Contains(i))
                    CreatePlayer(i);
                if (createEnemies && EnemySpawnIndex.Contains(i))
                    CreateEnemy(i);
                if (createTreasures && TreasureIndex.Contains(i))
                    CreateTreasures(i);
                if (createExit && ExitIndex.Contains(i))
                    CreateExit(i);
            }
        }
        
        private void CreatePlayer(int i)
        {
            GameObject playerObj = GameObject.Instantiate(playerPrefab);
            playerObj.transform.position = IndexToPosition(i);
        }

        private void CreateTreasures(int i)
        {
            GameObject treasureObj = GameObject.Instantiate(treasurePrefab);
            treasureObj.transform.position = IndexToPosition(i);
            Treasures.Add(i, treasureObj.GetComponent<Treasure>());
        }

        private void CreateEnemy(int i)
        {
            GameObject enemyObj = GameObject.Instantiate(enemyPrefab);
            enemyObj.transform.position = IndexToPosition(i);
        }

        private void CreateExit(int i)
        {
            GameObject exitObj = GameObject.Instantiate(exitPrefab);
            exitObj.transform.position = IndexToPosition(i);
        }

        private Vector3 IndexToPosition(int index)
        {
            return new Vector3(index % width, index / width);
        }

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

        public void GetNextPos(Enemy enemy)
        {
            tileCandidate.Clear();
            enemy.NextPos = enemy.transform.position;

            //어그로 범위 밖이면 제자리 멈춤.
            if(Vector3.Distance(enemy.transform.position, gameData.Player.transform.position) >= enemy.AggroRange)
            {
                enemy.NextPos = enemy.transform.position;
                walkedIndices.Add(GetIndex(enemy.NextPos));
                return;
            }

            if(Vector3.Distance(enemy.transform.position, gameData.Player.NextPos) <= 1f 
                || Vector3.Distance(enemy.transform.position, gameData.Player.transform.position) <= 1f)
            {
                enemy.NextPos = enemy.transform.position;
                walkedIndices.Add(GetIndex(enemy.NextPos));
                return;
            }

            for (int i = 0; i < checkList.Count; ++i)
            {
                if (CheckWalkablePosition(enemy.transform.position + checkList[i])
                    && EveUtil.IsIn180Angle(enemy.transform.position, gameData.Player.NextPos, enemy.transform.position + checkList[i]))
                {
                    tileCandidate.Add(enemy.transform.position + checkList[i]);
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
                    enemy.NextPos = tileCandidate[i];
                    walkedIndices.Add(GetIndex(enemy.NextPos));
                    return;
                }
            }

            if (enemy.NextPos == enemy.transform.position)
            {
                walkedIndices.Add(GetIndex(enemy.NextPos));
            }
        }

        /// <summary>
        /// Character 가 어느지점에 도착할지 기억해 놓는 인덱스 초기화.
        /// </summary>
        public void ClearWalkedIndices()
        {
            walkedIndices.Clear();
        }
    }
}