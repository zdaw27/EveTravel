using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EveTravel
{

    public class FSM<T> where T : MonoBehaviour
    {
        private T owner;
        private IState<T> current;
        private Dictionary<Type, IState<T>> states = new Dictionary<Type, IState<T>>();
        private bool debug = false;

        public FSM(T owner, IState<T> entry, bool debug = false)
        {
            this.debug = debug;
            this.owner = owner;
            current = entry;
            AddState(entry);
        }

        public void AddState(IState<T> state)
        {
            states.Add(state.GetType(), state);
        }

        public bool CheckCurrentState(Type type)
        {
            Type currentType = current.GetType();
            return current.GetType() == type;
        }

        public P GetState<P>() where P : IState<T>
        {
            return (P)states[typeof(P)];
        }

        public void StartFSM()
        {
            current.Enter(owner);
        }

        public void ChangeState<P>() where P : IState<T>
        {
            if (debug)
                Debug.Log("ChangedState : " + typeof(P));

            current.Exit(owner);
            current = states[typeof(P)];
            current.Enter(owner);
            current.Update(owner);
        }

        public void Update()
        {
            current.Update(owner);
        }
    }
}