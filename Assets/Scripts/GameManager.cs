using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace EveTravel
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UIObserver uiObserver;
        [SerializeField] private GameData gameData;

        private FSM<GameManager> fsm;

        public FSM<GameManager> Fsm { get { return fsm; } private set { } }
        public GameData GameData { get => gameData; set => gameData = value; }

        private void Awake()
        {
            fsm = new FSM<GameManager>(this, new InputState(GameData, uiObserver));
            fsm.AddState(new SortState());
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