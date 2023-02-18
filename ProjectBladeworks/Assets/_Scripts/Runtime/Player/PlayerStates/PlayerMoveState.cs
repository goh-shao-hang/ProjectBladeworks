using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Player
{
    public class PlayerMoveState : PlayerBaseState
    {
        public PlayerMoveState(FiniteStateMachine context, StateFactory stateFactory, Player player) : base(context, stateFactory, player)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }
    }
}