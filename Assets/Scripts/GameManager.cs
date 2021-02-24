using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace EveTravel
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private UIObserver uiObserver;
        [SerializeField]
        private GameData gameData;
        [SerializeField]
        private EffectRaiser effectRaiser;
        [SerializeField]
        private MapTable mapTable;
        [SerializeField]
        private Inventory inventory;
        [Header("Events")]
        [SerializeField]
        private GameEvent goldChangeEvent;
        [SerializeField]
        private GameEvent potionCountChangeEvent;
        [SerializeField]
        private GameEvent gameOverEvent;
        [SerializeField]
        private GameEvent gameStartEvent;
        [SerializeField]
        private GameEvent playerLevelChangedEvent;
        [SerializeField]
        private GameEvent playerStatChangedEvent;
        [SerializeField]
        private GameEvent introEvent;

        private FSM<GameManager> fsm;

        public FSM<GameManager> Fsm { get { return fsm; } private set { } }
        public GameData GameData { get => gameData; private set => gameData = value; }
        public EffectRaiser EffectRaiser { get => effectRaiser; private set => effectRaiser = value; }
        public UIObserver UiObserver { get => uiObserver; private set => uiObserver = value; }
        public MapTable MapTable { get => mapTable; private set => mapTable = value; }
        public Inventory Inventory { get => inventory; private set => inventory = value; }

        private void Awake()
        {
            fsm = new FSM<GameManager>(this, new ReadyState(), true);
            fsm.AddState(new IntroState());
            fsm.AddState(new InputState(this));
            fsm.AddState(new PathFindState());
            fsm.AddState(new BattleState());
            fsm.AddState(new GameOverState());
        }

        private void Start()
        {
            GameStart();
            fsm.StartFSM();
            playerStatChangedEvent.Raise();
            playerLevelChangedEvent.Raise();
            //debug
        }

        private void Update()
        {
            fsm.Update();
        }

        public void GameOver()
        {
            gameData.IsPlay = false;
            gameOverEvent.Raise();
        }

        public void GameStart()
        {
        }

        public void PlayerLevelUP()
        {
            playerStatChangedEvent.Raise();
        }

        public void UsePotion()
        {
            if (fsm.CheckCurrentState<InputState>() && Inventory.Potion > 0 && gameData.Player.Stat.hp < gameData.Player.Stat.maxHp)
            {
                Inventory.Potion--;
                potionCountChangeEvent.Raise();
                CharacterStat playerStat = gameData.Player.Stat;
                playerStat.hp += 50;
                if (playerStat.hp > playerStat.maxHp)
                    playerStat.hp = playerStat.maxHp;
                gameData.Player.Stat = playerStat;
                effectRaiser.RaiseEffect(gameData.Player.transform.position, EffectManager.EffectType.HealingEffect);
                playerStatChangedEvent.Raise();
            }
        }

        public void Battle()
        {
            for (int i = 0; i < GameData.Enemys.Count; ++i)
            {
                GameData.Enemys[i].GetAttackTarget();
            }

            bool isShake = false;
            for (int i = 0; i < GameData.Enemys.Count; ++i)
            {
                if (GameData.Enemys[i].HasTarget())
                {
                    GameData.Enemys[i].Attack();

                    if (!isShake)
                    {
                        isShake = true;
                        Camera.main.DOShakePosition(0.5f, 1f, 20, 90, true);
                    }
                }
            }

            if (GameData.Player.HasTarget())
            {
                GameData.Player.Attack();
            }

            //전투 후, 죽은 Enemy들 Container에서 제외 및 DeathState로 처리.
            for (int i = 0; i < GameData.Enemys.Count; ++i)
            {
                if (GameData.Enemys[i].Stat.hp <= 0)
                {
                    Enemy enemy = GameData.Enemys[i];
                    GameData.Enemys.Remove(enemy);
                    --i;
                    enemy.Death();
                    gameData.Player.EarnExp();
                }
            }

            if (gameData.Exp >= 100)
            {
                gameData.Player.LevelUP();
                gameData.IsPlay = false;
            }
            playerStatChangedEvent.Raise();
        }

        public void Intro()
        {
            introEvent.Raise();
        }

        public void LevelUpEnd()
        {
            gameData.IsPlay = true;
        }

        public void CheckPlayerInteraction()
        {
            if (gameData.Player.PlayerInteraction(gameData.EveMap))
            {
                ChangeMap();
            }
        }

        private void ChangeMap()
        {
            for (int i = 0; i < gameData.Enemys.Count; ++i)
            {
                GameObject.Destroy(gameData.Enemys[i].gameObject);
            }
            gameData.StageLevel++;
            GameObject.Destroy(gameData.EveMap.gameObject);
            GameObject.Instantiate(mapTable.Maps[gameData.StageLevel]);
        }

        public void PathFinding()
        {
            gameData.EveMap.ClearWalkedIndices();
            //가까운 몬스터 먼저 패스파인딩 완료 하기위해 정렬.
            gameData.Enemys.Sort((Enemy a, Enemy b) =>
            {
                if (Vector3.Distance(a.transform.position, gameData.Player.NextPos) < Vector3.Distance(b.transform.position, gameData.Player.NextPos))
                {
                    return -1;
                }
                else if (Vector3.Distance(a.transform.position, gameData.Player.NextPos) > Vector3.Distance(b.transform.position, gameData.Player.NextPos))
                    return 1;
                else

                    return 0;
            });

            for (int i = 0; i < gameData.Enemys.Count; ++i)
                gameData.Enemys[i].GetNextPos();
        }

        public bool CheckAllCharacterMoveComplete()
        {
            for (int i = 0; i < gameData.Enemys.Count; ++i)
            {
                if (gameData.Enemys[i].transform.position != gameData.Enemys[i].NextPos)
                {
                    return false;
                }
            }

            if (gameData.Player.transform.position != gameData.Player.NextPos)
                return false;

            return true;
        }

        public void StartCharactersMove()
        {
            for (int i = 0; i < gameData.Enemys.Count; ++i)
            {
                if (gameData.Enemys[i].transform.position != gameData.Enemys[i].NextPos)
                    gameData.Enemys[i].Move();
            }

            if (!gameData.Player.HasTarget())
                gameData.Player.Move();
        }
    }
}