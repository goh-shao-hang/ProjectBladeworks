using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Player
{
    public class PlayerIdleState : PlayerBaseState
    {
        public PlayerIdleState(FiniteStateMachine context, StateFactory stateFactory, Player player) : base(context, stateFactory, player)
        {
        }

        public override void CheckSwitchState()
        {
            base.CheckSwitchState();

            //if (_context.ii)
        }
    }
}