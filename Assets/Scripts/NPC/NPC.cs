using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace EveTravel
{
    public class NPC : MonoBehaviour
    {
        [SerializeField] private UIObserver UIopserver;
        [SerializeField] private GameData gameData;
        [SerializeField] private NPCStat stat;
        [SerializeField] private Animator animator;
        [SerializeField] private Seeker seeker;

        protected FSM<NPC> fsm;

        public Animator Animator { get { return animator; } }
        public NPCStat Stat { get { return stat; } set { stat = value; } }
        public Path Path { get; set; }
        public FSM<NPC> Fsm { get { return fsm; } set { fsm = value; } }
        public Vector3 NextPos { get; set; }
        public Seeker Seeker { get { return seeker; } set { seeker = value; } }
        public GameData GameData { get { return gameData; } private set { } }
    }
}