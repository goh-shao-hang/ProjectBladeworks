using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells
{
    public abstract class BaseState
    {
        protected FiniteStateMachine _ctx;
        public BaseState(FiniteStateMachine context)
        {
            this._ctx = context;
        }

        public virtual void Enter() { }

        public virtual void Execute()
        {
            CheckSwitchState();
        }

        public virtual void Exit() { }
        public virtual void CheckSwitchState() { }
        public virtual void InitializeSubStates() { }

        private void UpdateStates() { }
        private void SwitchState(BaseState newState) 
        {
            _ctx.ChangeState(newState);
        }
        private void SetSubState() { }
        private void SetSuperState() {}
    }
}