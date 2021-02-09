using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Pathfinding;

namespace EveTravel
{
    public class FSM<T> where T : MonoBehaviour
    {
        private T owner;
        private IState<T> current;
        private Dictionary<Type, IState<T>> states = new Dictionary<Type, IState<T>>();

        public FSM(T owner, IState<T> entryState)
        {
            this.owner = owner;
            current = entryState;
            states.Add(entryState.GetType(), entryState);
        }

        public FSM<T> AddState<P>(P state) where P : IState<T>
        {
            states.Add(state.GetType(), state);
            return this;
        }

        public P GetState<P>()
        {
            return (P)states[typeof(P)];
        }

        public void ChangeState(Type type)
        {
            current.Exit(owner);
            current = states[type];
            current.Enter(owner);
            //Debug.Log(owner.name + current.ToString());
        }

        public bool CheckCurrentState(Type type)
        {
            return current.GetType() == type;
        }

        public void StartFSM()
        {
            current.Enter(owner);
        }

        public void Update()
        {
            current.Update(owner);
        }
    }
}