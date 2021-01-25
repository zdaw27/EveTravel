using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] NPC player;
        [SerializeField] NPC monster;

        private FSM<GameManager> fsm;

        private void Awake()
        {
            fsm = new FSM<GameManager>(this, new PlayerState());
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