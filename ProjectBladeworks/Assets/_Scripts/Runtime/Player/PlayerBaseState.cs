using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Player
{
    public class PlayerBaseState : BaseState
    {
        protected Player _player;

        public PlayerBaseState(FiniteStateMachine context, StateFactory stateFactory, Player player) : base(context, stateFactory)
        {
            this._player = player;
        }
    }
}