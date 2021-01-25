using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] NPC player;
        [SerializeField] NPC monster;
        [SerializeField] UIObserver uiObserver;

        public NPC Player { get { return player; } private set { } }
        public FSM<GameManager> Fsm { get { return fsm; } private set { } }

        private FSM<GameManager> fsm;

        private void Awake()
        {
            fsm = new FSM<GameManager>(this, new PlayerState(Player, uiObserver));
            fsm.AddState<ReadyState>(new ReadyState());
            
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