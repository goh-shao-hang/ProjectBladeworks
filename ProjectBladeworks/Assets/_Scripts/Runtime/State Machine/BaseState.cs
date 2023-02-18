using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells
{
    public abstract class BaseState
    {
        protected FiniteStateMachine _context;
        protected StateFactory _stateFactory;
        public BaseState(FiniteStateMachine context, StateFactory stateFactory)
        {
            this._context = context;
            this._stateFactory = stateFactory;
        }

        public virtual void Enter() { }

        public virtual void Execute() { }
        public virtual void Exit() { }
        public virtual void CheckSwitchState() { }
        public virtual void InitializeSubStates() { }

        private void UpdateStates() { }
        private void SwitchState(BaseState newState) 
        {
            _context.ChangeState(newState);
        }
        private void SetSubState() { }
        private void SetSuperState() {}
    }
}