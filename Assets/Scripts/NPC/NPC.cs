using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

namespace EveTravel
{
    public abstract class NPC : MonoBehaviour
    {
        [SerializeField] protected UIObserver UIopserver;
        [SerializeField] protected GameData gameData;
        [SerializeField] protected NPCStat stat;
        [SerializeField] protected Animator animator;
        [SerializeField] protected Seeker seeker;
        [SerializeField] protected SingleNodeBlocker blocker;

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