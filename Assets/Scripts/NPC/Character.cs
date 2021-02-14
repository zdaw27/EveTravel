using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] protected UIObserver UIopserver;
        [SerializeField] protected GameData gameData;
        [SerializeField] protected CharacterStat stat;
        [SerializeField] protected Animator animator;
        [SerializeField] protected EffectListener effectListener;

        protected FSM<Character> fsm;
        protected Character attackTarget;

        public Animator Animator { get { return animator; } }
        public CharacterStat Stat { get { return stat; } set { stat = value; } }
        public FSM<Character> Fsm { get { return fsm; } set { fsm = value; } }
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

        abstract public void Attack();
        virtual public void SetTarget(Character character)
        {
            attackTarget = character;
        }

        virtual public bool HasTarget()
        {
            return !(attackTarget is null);
        }

        
    }
}
