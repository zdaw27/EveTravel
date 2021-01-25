using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace EveTravel
{
    public class NPC : MonoBehaviour
    {
        [SerializeField] private UIObserver UIopserver;
        [SerializeField] private NPCStat _stat;
        [SerializeField] private Animator _animator;
        [SerializeField] private Seeker _seeker;

        private FSM<NPC> fsm;

        public Animator Animator { get { return _animator; } }
        public NPCStat Stat { get { return _stat; } set { _stat = value; } }
        public Seeker Seeker { get { return _seeker; } set { _seeker = value; } }
        public Path Path { get; set; }
        public FSM<NPC> Fsm { get { return fsm; } set { fsm = value; } }

        private void Awake()
        {
            fsm = new FSM<NPC>(this, new IdleState());
            fsm.AddState(new MoveState());
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