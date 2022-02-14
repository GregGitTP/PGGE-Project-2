using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns
{
    public class State{
        protected FSM fsm;

        public State(FSM _fsm){
            fsm = _fsm;
        }

        public virtual void Enter(){}
        public virtual void Exit(){}
        public virtual void Update(){}
        public virtual void FixedUpdate(){}
        public virtual void LateUpdate(){}
    }

    public class FSM{
        protected Dictionary<int, State> states = new Dictionary<int, State>();
        protected State currentState;

        public FSM(){}

        public void Add(int key, State _state){
            states.Add(key, _state);
        }

        public State GetState(int key){
            return states[key];
        }

        public State GetCurrentState(){
            return currentState;
        }

        public void SetCurrentState(State _state){
            if(currentState != null) currentState.Exit();
            currentState = _state;
            if(currentState != null) currentState.Enter();
        }

        public void Update(){
            if(currentState != null) currentState.Update(); 
        }

        public void FixedUpdate(){
            if(currentState != null) currentState.FixedUpdate();
        }

        public void LateUpdate(){
            if(currentState != null) currentState.LateUpdate();
        }
    }
}