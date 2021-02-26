using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace EveTravel
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private JoystickObserver joystickObserver;
        [SerializeField]
        private GameData gameData;
        [SerializeField]
        private EffectRaiser effectRaiser;
        [SerializeField]
        private MapTable mapTable;
        [SerializeField]
        private Inventory inventory;
        [SerializeField]
        private bool debugFSM = true;
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
        private BaseEffect aimEffect;
        private bool isAttackButtonPushed = false;

        public FSM<GameManager> Fsm { get { return fsm; } private set { } }
        public GameData GameData { get => gameData; private set => gameData = value; }
        public EffectRaiser EffectRaiser { get => effectRaiser; private set => effectRaiser = value; }
        public JoystickObserver JoystickObserver { get => joystickObserver; private set => joystickObserver = value; }
        public MapTable MapTable { get => mapTable; private set => mapTable = value; }
        public Inventory Inventory { get => inventory; private set => inventory = value; }
        public bool IsAttackButtonPushed { get => isAttackButtonPushed; private set => isAttackButtonPushed = value; }

        private void Awake()
        {
            fsm = new FSM<GameManager>(this, new IntroState(), debugFSM);

            fsm.AddState(new ReadyState());
            fsm.AddState(new MapChangeState());
            fsm.AddState(new InputState());
            
            fsm.AddState(new BattleState());
            fsm.AddState(new PathFindState());
            fsm.AddState(new LevelUpState());

            fsm.AddState(new GameOverState());
        }

        private void Start()
        {
            SetPlayFlag(true);
            fsm.StartFSM();
            playerStatChangedEvent.Raise();
            playerLevelChangedEvent.Raise();
        }

        private void Update()
        {
            fsm.Update();
        }

        private void LateUpdate()
        {
            //UI JoysickDir , Attack flag 해제.
            //추후에 UI InputContoller 추가예정.
            IsAttackButtonPushed = false;
            joystickObserver.JoyStickDir = JoyStickDir.None;
        }

        public void GameOver()
        {
            gameData.IsPlay = false;
            gameOverEvent.Raise();
        }

        public void SetPlayFlag(bool flag)
        {
            gameData.IsPlay = flag;
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
                if (GameData.Player.AttackTarget.Stat.hp <= 0)
                    RemoveAim();
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
            
            playerStatChangedEvent.Raise();
        }

        public void LevelUp()
        {
            gameData.Player.LevelUP();
        }

        public void GameRestart()
        {
            gameData.StageLevel = 0;
            GameObject.Destroy(gameData.Player.gameObject);
            gameData.Player = null;
            ChangeMap();
            fsm.ChangeState<ReadyState>();
        }

        public void Intro()
        {
            gameData.StageLevel = 0;
            gameData.IsPlay = false;
            CreateNewMap();
            introEvent.Raise();
        }

        public void LevelUpEnd()
        {
            gameData.IsPlay = true;
        }

        public void CheckPlayerInteraction()
        {
            gameData.Player.PlayerInteraction(gameData.EveMap);
        }

        public bool CheckNextLevel()
        {
            return gameData.Player.CheckNextLevel(gameData.EveMap);
        }

        public void ChangeMap()
        {
            for (int i = 0; gameData.Enemys.Count != 0;)
            {
                GameObject.Destroy(gameData.Enemys[i].gameObject);
            }
            gameData.Enemys.Clear();
            gameData.EveMap.ClearStuff();

            GameObject.Destroy(gameData.EveMap.gameObject);
            GameObject.Instantiate(mapTable.Maps[gameData.StageLevel]);
        }

        public void CreateNewMap()
        {
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

        public Vector3 GetAimPos()
        {
            if (aimEffect)
                return aimEffect.transform.position;
            else
                return Vector3.left * 1000f;
        }

        public void SetAim(Vector3 aimPos)
        {
            Debug.Log("SetAim");
            if (!aimEffect)
            {
                aimEffect = effectRaiser.RaiseEffect(aimPos, EffectManager.EffectType.PermanentEffect);
            }
            else
            {
                aimEffect.EndEffect();
                aimEffect = effectRaiser.RaiseEffect(aimPos, EffectManager.EffectType.PermanentEffect);
            }
        }
        
        public void RemoveAim()
        {
            if(aimEffect)
                aimEffect.EndEffect();
            aimEffect = null;
        }

        public void OnAttackButtonClick()
        {
            IsAttackButtonPushed = true;
        }

        public bool CheckJoystickPushed()
        {
            if (joystickObserver.JoyStickDir == JoyStickDir.None)
                return false;
            else
                return true;
        }
    }
}