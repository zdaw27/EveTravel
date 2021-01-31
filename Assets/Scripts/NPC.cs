using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace EveTravel
{
    public class NPC : MonoBehaviour
    {
        [SerializeField] protected UIObserver UIopserver;
        [SerializeField] protected GameData gameData;
        [SerializeField] protected NPCStat _stat;
        [SerializeField] protected Animator _animator;
        [SerializeField] private Seeker _seeker;

        protected FSM<NPC> fsm;

        public Animator Animator { get { return _animator; } }
        public NPCStat Stat { get { return _stat; } set { _stat = value; } }
        public Path Path { get; set; }
        public FSM<NPC> Fsm { get { return fsm; } set { fsm = value; } }
        public Vector3 NextPos { get; set; }
        public Seeker Seeker { get { return _seeker; } set { _seeker = value; } }
        public GameData GameData { get { return gameData; } private set { } }
        
        
    }
}