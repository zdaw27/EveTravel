using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class NPC : MonoBehaviour
    {
        [SerializeField] private NPCStat _stat;
        [SerializeField] private Animator _animator;

        public Animator Animator { get { return _animator; } }
        public NPCStat Stat { get { return _stat; } set { _stat = value; } }

        private FSM fsm;

        private void Awake()
        {
            fsm = new FSM(this , new IdleState());
        }

        // Start is called before the first frame update
        void Start()
        {
            fsm.StartFSM();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}