using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public abstract class NPC : MonoBehaviour
    {
        [SerializeField] protected UIObserver UIopserver;
        [SerializeField] protected GameData gameData;
        [SerializeField] protected NPCStat stat;
        [SerializeField] protected Animator animator;

        protected FSM<NPC> fsm;

        public Animator Animator { get { return animator; } }
        public NPCStat Stat { get { return stat; } set { stat = value; } }
        public FSM<NPC> Fsm { get { return fsm; } set { fsm = value; } }
        public Vector3 NextPos { get; set; }
        public GameData GameData { get { return gameData; } private set { } }

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
