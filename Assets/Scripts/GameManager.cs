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
        private GameEvent playerLevelUpEvent;

        private FSM<GameManager> fsm;

        public FSM<GameManager> Fsm { get { return fsm; } private set { } }
        public GameData GameData { get => gameData; set => gameData = value; }
        public EffectRaiser EffectRaiser { get => effectRaiser; private set => effectRaiser = value; }
        public UIObserver UiObserver { get => uiObserver; set => uiObserver = value; }
        public MapTable MapTable { get => mapTable; set => mapTable = value; }
        public Inventory Inventory { get => inventory; set => inventory = value; }

        private void Awake()
        {
            fsm = new FSM<GameManager>(this, new ReadyState(), true);
            fsm.AddState(new InputState(this));
            fsm.AddState(new PathFindState());
            fsm.AddState(new MoveNPCState());
            fsm.AddState(new BattleState());
            fsm.AddState(new GameOverState());
        }

        private void Start()
        {
            GameStart();
            fsm.StartFSM();
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
                }
            }
        }
    }
}