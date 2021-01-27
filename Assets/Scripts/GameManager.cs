using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] Player player;
        [SerializeField] Enemy enemy;
        [SerializeField] UIObserver uiObserver;
        [SerializeField] GameData gameData;

        public Player Player { get { return player; } private set { } }
        public Enemy Enemy { get { return enemy; } private set { } }
        public FSM<GameManager> Fsm { get { return fsm; } private set { } }

        private FSM<GameManager> fsm;

        private void Awake()
        {
            gameData.Enemys.Add(enemy);
            gameData.player = player;
            fsm = new FSM<GameManager>(this, new InputState(Player, uiObserver));
            fsm.AddState(new PlayState());
            fsm.AddState(new ReadyState());

        }
        // Start is called before the first frame update
        void Start()
        {
            fsm.StartFSM();
        }

        // Update is called once per frame
        void Update()
        {
            fsm.Update();
        }
    }
}