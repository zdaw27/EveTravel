﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EveTravel
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UIObserver uiObserver;
        [SerializeField] private GameData gameData;
        [SerializeField] private EffectListener effectListener;

        private FSM<GameManager> fsm;

        public FSM<GameManager> Fsm { get { return fsm; } private set { } }
        public GameData GameData { get => gameData; set => gameData = value; }
        public EffectListener EffectListener { get => effectListener; private set => effectListener = value; }
        public UIObserver UiObserver { get => uiObserver; set => uiObserver = value; }

        private void Awake()
        {
            fsm = new FSM<GameManager>(this, new ReadyState(), true);
            fsm.AddState(new InputState(this));
            fsm.AddState(new PathFindState());
            fsm.AddState(new MoveNPCState());
        }

        void Start()
        {
            fsm.StartFSM();
        }

        void Update()
        {
            fsm.Update();
        }
    }
}