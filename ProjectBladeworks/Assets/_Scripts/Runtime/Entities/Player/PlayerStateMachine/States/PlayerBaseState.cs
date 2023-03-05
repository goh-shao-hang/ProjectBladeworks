using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCells.Entities.Player
{
    public class PlayerBaseState : BaseState
    {
        protected Player _player;

        public PlayerBaseState(FiniteStateMachine context, Player player) : base(context)
        {
            this._player = player;
        }
    }
}