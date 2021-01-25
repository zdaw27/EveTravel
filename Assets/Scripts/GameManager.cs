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

        private FSM<GameManager> fsm;

        private void Awake()
        {
            fsm = new FSM<GameManager>(this, new PlayerState(Player, uiObserver.OnJoyStick));
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}